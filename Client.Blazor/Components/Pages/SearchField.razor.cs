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

        SearchStudentModel studentFields = new SearchStudentModel();

        List<ClassInformationModel>? classes;
        List<StudentProfileModel>? students;

        private bool visible = false;

        private void CloseSearchPopup()
        {
            visible = false;
        }

        string? errorMessage;
        int total;

        bool isOpenSBId = false;
        bool isOpenSBName = false;
        bool isOpenSBDate = false;
        bool isOpenSBAddress = false;
        bool isOpenSBClass = false;

        void OpenSearchByIdField()
        {
            isOpenSBId = true;
            isOpenSBName = false;
            isOpenSBDate = false;
            isOpenSBAddress = false;
            isOpenSBClass = false;
            studentFields= new SearchStudentModel();
        }

        void OpenSearchByNameField()
        {
            isOpenSBId = false;
            isOpenSBName = true;
            isOpenSBDate = false;
            isOpenSBAddress = false;
            isOpenSBClass = false;
            studentFields = new SearchStudentModel();
        }

        void OpenSearchByDateField()
        {
            isOpenSBId = false;
            isOpenSBName = false;
            isOpenSBDate = true;
            isOpenSBAddress = false;
            isOpenSBClass = false;
            studentFields = new SearchStudentModel();
        }

        void OpenSearchByAddressField()
        {
            isOpenSBId = false;
            isOpenSBName = false;
            isOpenSBDate = false;
            isOpenSBAddress = true;
            isOpenSBClass = false;
            studentFields = new SearchStudentModel();
        }

        void OpenSearchByClassField()
        {
            isOpenSBId = false;
            isOpenSBName = false;
            isOpenSBDate = false;
            isOpenSBAddress = false;
            isOpenSBClass = true;
            studentFields = new SearchStudentModel();
        }

        private async Task LoadClassesAsync()
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

        // not work
        private async Task HandleOnSearchAsync()
        {
            if (studentFields.Id.HasValue)
            {
                await GetByIdAsync();
            }
            if (!string.IsNullOrEmpty(studentFields.Name))
            {
                await GetByNameAsync();
            }
            if (!string.IsNullOrEmpty(studentFields.Address))
            {
                await GetByAddressAsync();
            }
            if (studentFields.StartDate.HasValue && studentFields.EndDate.HasValue)
            {
                await GetByDateAsync();
            }
            if((studentFields.ClassId.HasValue))
            {
                await GetByClassAsync();
            }
            visible = true;
        }

        private async Task GetByIdAsync()
        {
            var reply = await StudentService.GetProfileAsync(new IdRequest { Id = studentFields.Id!.Value });
            if (reply.Student == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                total = 1;
                students = new List<StudentProfileModel>()
                {
                    new StudentProfileModel
                    {
                        Id = reply.Student.Id,
                        FullName = reply.Student.FullName,
                        Birthday = reply.Student.Birthday,
                        Address = reply.Student.Address,
                        ClassId = reply.Student.ClassId,
                        ClassName = reply.Student.ClassName,
                    }
                };
            }
        }

        private async Task GetByNameAsync()
        {
            var reply = await StudentService.GetProfileByNameAsync(new NameRequest { Name = studentFields.Name! });
            if (reply.Students == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                total = reply.Count;
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();
            }
        }

        private async Task GetByAddressAsync()
        {
            var reply = await StudentService.GetProfileByAddressAsync(new AddressRequest { Address = studentFields.Address! });
            if (reply.Students == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                total = reply.Count;
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();
            }
        }

        private async Task GetByDateAsync()
        {
            var reply = await StudentService.GetProfileByDateAsync(new DateRequest { DateStart = studentFields.StartDate!.Value, DateEnd = studentFields.EndDate!.Value });
            if (reply.Students == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                total = reply.Count;
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();
            }
        }

        private async Task GetByClassAsync()
        {
            var reply = await StudentService.GetProfileByClassAsync(new IdRequest { Id = studentFields.ClassId!.Value});
            if (reply.Students == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                total = reply.Count;
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}
