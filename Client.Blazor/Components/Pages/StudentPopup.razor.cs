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
        IStudentService StudentService { get; set; } = null!;

        [Inject]
        IClassService ClassService { get; set; } = null!;

        [Inject]
        IMapper Mapper { get; set; } = null!;

        [Inject]
        NotificationService Notification { get; set; } = null!;

        List<ClassInfoDTO> classes = new List<ClassInfoDTO>();

        async Task ClosePopupAsync()
        {
            IsDetails = false;
            IsCreate = false;
            Visible = false;
            await OnClose.InvokeAsync();
        }

        async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfoAsync(new Shared.Empty());
            if (reply.Classes == null)
            {
                _ = NotificationMessage(reply.Message, false);
            }
            else
            {
                classes = Mapper.Map<List<ClassInfoDTO>>(reply.Classes);
            }
        }
        

        async Task CreateOrUpdateAsync()
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
            _ = NotificationMessage(reply.Message, reply.Success);
            await ReloadStudents.InvokeAsync();
            await ClosePopupAsync();
        }

        Task NotificationMessage(string? message, bool isSuccess)
        {
            _ = Notification.Open(new NotificationConfig()
            {
                Message = "Success",
                Description = message != null ? message : IsCreate ? "Created" : "Updated",
                NotificationType = isSuccess ? NotificationType.Success : NotificationType.Error
            });
            return Task.CompletedTask;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}