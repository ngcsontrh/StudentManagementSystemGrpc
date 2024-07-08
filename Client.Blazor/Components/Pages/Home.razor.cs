using AntDesign;
using AutoMapper;
using Client.Blazor.DTOs;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        // models
        private StudentProfileDTO? student;
        SearchStudentDTO searchFields = new SearchStudentDTO();
        private List<StudentProfileDTO>? students;

        private int pageNumber = 1;
        private int pageSize = 10;
        int total; // total students in db

        bool isVisible = false;
        string? popupStatus;

        private void OpenPopup(string status, StudentProfileDTO? student = null)
        {
            popupStatus = status;
            this.student = student ?? new StudentProfileDTO();
            isVisible = true;
        }

        private async Task ClosePopup()
        {
            await Task.Run(() =>
            {
                popupStatus = null;
                isVisible = false;
                this.student = null;
            });
        }

        private async Task LoadStudentsAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchFields);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;
            var reply = await StudentService.GetPaginationAsync(request);
            if(reply.Students != null)
            {
                students = Mapper.Map<List<StudentProfileDTO>>(reply.Students);
                total = reply.Count;
            }
            else
            {
                if (reply.Count != 0)
                {
                    pageNumber -= 1;
                    await LoadStudentsAsync();
                }
                else
                {
                    _ = Notification.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = reply.Message,
                        NotificationType = NotificationType.Error
                    });
                    searchFields = new SearchStudentDTO();
                }
            }
        }

        private async Task HandlePageIndexChange(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            await LoadStudentsAsync();
        }

        private async Task HandlePageSizeChange(PaginationEventArgs args)
        {
            pageNumber = 1;
            pageSize = args.PageSize;
            await LoadStudentsAsync();
        }

        private void OnSort(MenuItem menu)
        {
            switch(menu.Key)
            {
                case "sbName": 
                    students = students!.OrderBy(s => s.FullName).ToList(); 
                    break;
                case "sbId": 
                    students = students!.OrderBy(s => s.Id).ToList(); 
                    break;
                case "sbBirthday":
                    students = students!.OrderBy(s => s.Birthday).ToList();
                    break;
                case "sbAddress":
                    students = students!.OrderBy(s => s.Address).ToList();
                    break;
                case "sbClassId":
                    students = students = students!.OrderBy(s => s.ClassId).ToList(); 
                    break;
                case "sbClassName":
                    students = students = students!.OrderBy(s => s.ClassName).ToList();
                    break;
            }
        }

        private async Task OnSearch(SearchStudentDTO searchStudent)
        {
            pageNumber= 1;
            searchFields = searchStudent;
            await LoadStudentsAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
        }
    }
}