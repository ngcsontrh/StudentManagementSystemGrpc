using AntDesign;
using AutoMapper;
using Client.Blazor.DTOs;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class StudentPopup : ComponentBase
    {
        [Parameter]
        public StudentProfileDTO Student { get; set; } = null!;

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public string Status { get; set; } = null!;

        [Parameter]
        public EventCallback ReloadStudents { get; set; }

        [Parameter]
        public EventCallback<string> OnClose { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        List<ClassInfoDTO>? classes;

        private async Task ClosePopup()
        {
            await OnClose.InvokeAsync(Status);
            IsVisible = false;
        }

        private async Task HandleOnSubmit()
        {
            switch (Status)
            {
                case "Create":
                case "Update":
                    await CreateOrUpdateAsync(); 
                    break;
                case "Delete":
                    await DeleteStudentAsync();
                    break;
                case "Details":
                    await Task.Run(() => { });
                    break;
            }
        }

        async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfo(new Shared.Empty());
            if (reply.Classes == null)
            {
                NotificationMessage(reply.Message, false);
            }
            else
            {
                classes = Mapper.Map<List<ClassInfoDTO>>(reply.Classes);
            }
        }

        // delete student by id
        private async Task DeleteStudentAsync()
        {
            var reply = await StudentService.DeleteAsync(new IdRequest { Id = Student.Id });
            NotificationMessage(reply.Message, reply.Success);
            await ReloadStudents.InvokeAsync();
            await ClosePopup();
        }

        private async Task CreateOrUpdateAsync()
        {
            var student = Mapper.Map<StudentProfile>(Student);
            OperationReply reply = new OperationReply();
            if(Status == "Create")
            {
                reply = await StudentService.CreateAsync(student);
            }
            else if(Status == "Update")
            {
                reply = await StudentService.UpdateAsync(student);
            }
            NotificationMessage(reply.Message, reply.Success);
            await ReloadStudents.InvokeAsync();
            await ClosePopup();
        }

        void NotificationMessage(string? message, bool isSuccess)
        {
           _ = Notification.Open(new NotificationConfig()
            {
                Message = "Success",
                Description = message ?? $"{Status}d",
                NotificationType = isSuccess ? NotificationType.Success : NotificationType.Error
            });
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}