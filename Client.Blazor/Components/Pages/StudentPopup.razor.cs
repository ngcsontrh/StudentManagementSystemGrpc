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
        public bool IsCreate { get; set; }

        [Parameter]
        public bool Visible {  get; set; }

        [Parameter]
        public bool IsDetails {  get; set; }

        [Parameter]
        public EventCallback ReloadStudents { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        List<ClassInfoDTO> classes = new List<ClassInfoDTO>();

        private async Task ClosePopup()
        {
            IsDetails = false;
            IsCreate = false;
            Visible = false;
            await OnClose.InvokeAsync();
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
            if(IsCreate)
            {
                reply = await StudentService.CreateAsync(student);
            }
            else
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
                Description = message != null ? message : IsCreate ? "Created" : "Updated",
                NotificationType = isSuccess ? NotificationType.Success : NotificationType.Error
            });
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}