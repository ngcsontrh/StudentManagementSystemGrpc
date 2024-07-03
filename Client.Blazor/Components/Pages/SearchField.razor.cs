using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared.Models;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class SearchField : ComponentBase
    {
        [Inject]
        IStudentService StudentService { get; set; } = null!;

        [Inject]
        IClassService ClassService { get; set; } = null!;

        [Parameter]
        public EventCallback<List<StudentProfileModel>> OnStudentFound { get; set; }

        [Parameter]
        public EventCallback OnStudentNotFound { get; set; }

        SearchStudentModel studentFields = new SearchStudentModel();

        List<ClassInformationModel>? classes;
        List<StudentProfileModel>? students;

        private async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfo(new Shared.Empty());
            classes = reply.Classes!.Select(c => new ClassInformationModel
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
            }).ToList();
        }

        private async Task HandleOnSearchAsync()
        {
            students = null;
            var reply = await StudentService.SearchStudentAsync(new SearchRequest
            {
                Id = studentFields.Id,
                Name = studentFields.Name,
                Address = studentFields.Address,
                ClassId = studentFields.ClassId,
                StartDate = studentFields.StartDate,
                EndDate = studentFields.EndDate
            });
            if (reply.Students == null)
            {
                await OnStudentNotFound.InvokeAsync();
            }
            else
            {
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();
                await OnStudentFound.InvokeAsync(students);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}
