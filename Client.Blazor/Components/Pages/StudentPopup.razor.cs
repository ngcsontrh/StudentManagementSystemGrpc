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
        public EventCallback OnClose { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        List<ClassInformationModel>? classes;

        private async Task ClosePopup()
        {
            await OnClose.InvokeAsync();
            IsVisible = false;
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
            if (Status == "Create")
            {
                StudentProfile = new StudentProfileModel();
            }
            await LoadClassesAsync();
        }

        private async Task CreateStudent()
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
                StudentProfile = new StudentProfileModel();
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Added new student",
                    NotificationType = NotificationType.Success
                });
                await ReloadStudents.InvokeAsync();
            }
            else
            {
                StudentProfile = new StudentProfileModel();
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }

            await ClosePopup();
        }

        private async Task UpdateStudent()
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
                StudentProfile = new StudentProfileModel();
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Updated",
                    NotificationType = NotificationType.Success
                });
                await ReloadStudents.InvokeAsync();
            }
            else
            {
                StudentProfile = new StudentProfileModel();
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            await ClosePopup();
        }

    }
}