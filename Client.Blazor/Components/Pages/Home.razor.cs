using AntDesign;
using Client.Blazor.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.DTOs;

namespace Client.Blazor.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation {  get; set; } = null!;

        [Inject]
        public NotificationService Notification {  get; set; } = null!;

        // models
        private StudentProfileModel student = null!;
        private List<StudentProfileModel>? students;
        private List<StudentProfileModel>? studentsSearched;

        private int pageNumber = 1;
        private int pageSize = 10;
        int total = 0; // total students in db

        bool isOpenUpdatePopup = false; // true if open update
        bool isOpenDetailsPopup = false; // true if open details
        bool isDeletePopup = false;
        bool isOpenCreatePopup = false;
        bool isStudentSearchedFound = false;

        private void OpenPopup(string status, StudentProfileModel? student = null)
        {
            this.student = student ?? new StudentProfileModel();
            switch (status)
            {
                case "Update":
                    isOpenUpdatePopup = true;
                    break;
                case "Create":
                    isOpenCreatePopup = true;
                    break;
                case "Details":
                    isOpenDetailsPopup = true;
                    break;
                case "Delete":
                    isDeletePopup = true;
                    break;
            }
        }

        private async Task ClosePopup(string status)
        {
            student = new StudentProfileModel();
            switch (status)
            {
                case "Update":
                    isOpenUpdatePopup = false;
                    break;
                case "Create":
                    isOpenCreatePopup = false;
                    break;
                case "Details":
                    isOpenDetailsPopup = false;
                    break;
                case "Delete":
                    isDeletePopup = false;
                    break;
            }
        }

        // load students with pagination
        private async Task LoadStudentsAsync()
        {
            var reply = await StudentService.GetWithPaginationAsync(new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            });

            if (reply.Students != null)
            {
                students = reply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                    ClassSubject = s.ClassSubject,
                    TeacherId = s.TeacherId,
                    TeacherBirthday = s.TeacherBirthday,
                    TeacherFullName = s.TeacherFullName
                }).ToList();
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
                }
            }
        }

        private async Task HandlePageIndexChange(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            if(isStudentSearchedFound)
            {
                students = studentsSearched.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                await LoadStudentsAsync();
            }
        }

        private async Task HandlePageSizeChange(PaginationEventArgs args)
        {
            pageSize = args.PageSize;
            if (!isStudentSearchedFound)
            {
                await LoadStudentsAsync();
            }
            else
            {
                students = studentsSearched.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
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

        private async Task OnSearchFound(StudentPaginatedModel paginatedModel)
        {
            pageNumber = 1;
            pageSize = 10;
            isStudentSearchedFound = true;
            studentsSearched = paginatedModel.Students?.OrderBy(s => s.Id).ToList();
            total = studentsSearched.Count;
            students = studentsSearched.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        private async Task OnSearchedNotFound()
        {
            await LoadStudentsAsync();
            isStudentSearchedFound = false;
            _ = Notification.Open(new NotificationConfig()
            {
                Message = "Error",
                Description = "Not Found",
                NotificationType = NotificationType.Error,
            });
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
        }
    }
}