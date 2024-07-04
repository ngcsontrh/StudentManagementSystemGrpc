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
        bool isOpenCreatePopup = false;
        bool isStudentSearchedFound = false;

        private void OpenUpdatePopup(StudentProfileModel student)
        {
            isOpenUpdatePopup = true;
            this.student = student;
        }

        private void OpenCreatePopup()
        {
            this.student = new StudentProfileModel()
            {
                Birthday = DateTime.Now
            };
            isOpenCreatePopup = true;
        }

        private void OpenDetailsPopup(StudentProfileModel student)
        {
            this.student = student;
            isOpenDetailsPopup = true;
        }

        private async Task CloseUpdatePopup()
        {
            isOpenUpdatePopup = false;
        }

        private async Task CloseCreatePopup()
        {
            isOpenCreatePopup = false;
        }

        private async Task CloseDetailsPopup()
        {
            isOpenDetailsPopup = false;
        }

        // load students with pagination
        private async Task LoadStudentsAsync()
        {
            var reply = await StudentService.GetWithPaginationAsync(new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            });

            if (reply.Students == null  && reply.Count == 0)
            {
                _ =  Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            else if(reply.Students != null)
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

        // delete student by id
        private async Task HandleOnDeleteAsync(int id)
        {
            var reply = await StudentService.DeleteAsync(new IdRequest { Id = id });
            if (reply.Success)
            {
                pageSize = 10;
                pageNumber = 1;
                await LoadStudentsAsync();
                if (students != null && students.Count == 1)
                {
                    pageNumber-= 1;
                }
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
        }

        private void HandleSortByName()
        {
            students = students!.OrderBy(s => s.FullName).ToList();
        }

        private void HandleSortById()
        {
            students = students!.OrderBy(s => s.Id).ToList();
        }

        private void HandleSortByBirthday()
        {
            students = students!.OrderBy(s => s.Birthday).ToList();
        }

        private void HandleSortByAddress()
        {
            students = students!.OrderBy(s => s.Address).ToList();
        }

        private void HandleSortByClassId()
        {
            students = students!.OrderBy(s => s.ClassId).ToList();
        }

        private void HandleSortByClassName()
        {
            students = students!.OrderBy(s => s.ClassName).ToList();
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