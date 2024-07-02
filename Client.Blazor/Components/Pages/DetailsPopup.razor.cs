using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class DetailsPopup : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public Action OnClose { get; set; } = null!;

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        private StudentDetailsModel studentDetails = null!;
        private string? errorMessage; 

        private void ClosePopup()
        {
            OnClose();
            IsVisible = false;
        }

        private async Task LoadDetails()
        {
            var reply = await StudentService.GetDetailsAsync(new IdRequest { Id = this.Id });
            if (reply.Student == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                errorMessage = null;
                studentDetails = new StudentDetailsModel
                {
                    Id = reply.Student.Id,
                    FullName = reply.Student.FullName,
                    Birthday = reply.Student.Birthday,
                    Address = reply.Student.Address,
                    ClassId = reply.Student.ClassId,
                    ClassName = reply.Student.ClassName,
                    ClassSubject = reply.Student.ClassSubject,
                    TeacherId = reply.Student.TeacherId,
                    TeacherFullName = reply.Student.TeacherFullName,
                    TeacherBirthday = reply.Student.TeacherBirthday,
                };
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadDetails();
        }
    }
}
