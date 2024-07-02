using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class UpdatePopup : ComponentBase
    {
        [Parameter]
        public StudentProfileModel StudentProfile { get; set; } = null!;

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback ReloadStudents { get; set; }

        [Parameter]
        public Action OnClose { get; set; } = null!;

        public void ClosePopup()
        {
            OnClose();
            IsVisible = false;
        }

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        List<ClassInformationModel>? classes;

        string? errorMessage;
        string? successMessage;

        async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfo(new Shared.Empty());
            if (reply.Classes == null)
            {
                errorMessage = reply.Message;
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
            errorMessage = null;
            await LoadClassesAsync();            
        }

        private async Task UpdateStudent()
        {
            var reply = await StudentService.UpdateAsync(new UpdateStudentRequest
            {
                Id = StudentProfile.Id,
                FullName = StudentProfile.FullName,
                Birthday = StudentProfile.Birthday,
                Address = StudentProfile.Address,
                ClassId = StudentProfile.ClassId
            });
            if (reply.Success)
            {
                successMessage = "Updated sucessfully";
                errorMessage = null;
                await ReloadStudents.InvokeAsync();
            }
            else
            {
                errorMessage = reply.Message;
            }
        }
    }
}