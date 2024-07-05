using AntDesign;
using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class StudentPopup : ComponentBase
    {
        [Parameter]
        public StudentProfileModel StudentProfile { get; set; } = null!;

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
        public NotificationService Notification { get; set; } = null!;

        List<ClassInformationModel>? classes;

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
                    await CreateStudentAsync();
                    break;
                case "Update":
                    await UpdateStudentAsync();
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
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                classes = reply.Classes.Select(c => new ClassInformationModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                }).ToList();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }

        private async Task CreateStudentAsync()
        {
            var reply = await StudentService.CreateAsync(new StudentProfile
            {
                FullName = StudentProfile.FullName,
                Birthday = StudentProfile.Birthday,
                Address = StudentProfile.Address,
                ClassId = StudentProfile.ClassId!
            });
            if (reply.Success)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Added new student",
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            await ReloadStudents.InvokeAsync();
            await ClosePopup();
        }

        // delete student by id
        private async Task DeleteStudentAsync()
        {
            var reply = await StudentService.DeleteAsync(new IdRequest { Id = StudentProfile.Id });
            if (reply.Success)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Student has been deleted",
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            await ReloadStudents.InvokeAsync();
            await ClosePopup();
        }

        private async Task UpdateStudentAsync()
        {
            var reply = await StudentService.UpdateAsync(new StudentProfile
            {
                Id = StudentProfile.Id,
                FullName = StudentProfile.FullName,
                Birthday = StudentProfile.Birthday,
                Address = StudentProfile.Address,
                ClassId = StudentProfile.ClassId!
            });
            if (reply.Success)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Updated",
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            await ReloadStudents.InvokeAsync();
            await ClosePopup();
        }

    }
}