using AntDesign;
using Client.Blazor.Models;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Models;

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
        
        private int studentDetailsId;

        private string? errorMessage;        

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
            isOpenCreatePopup = true;
        }

        private void OpenDetailsPopup(int id)
        {
            studentDetailsId = id;
            isOpenDetailsPopup = true;
        }

        private async Task CloseUpdatePopup(string errorMessage)
        {
            await Task.Run(() =>
            {
                isOpenUpdatePopup = false;
                this.errorMessage = errorMessage;
            });
            if(errorMessage == null)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = "Updated student information",
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = errorMessage,
                    NotificationType = NotificationType.Error
                });
            }
        }

        private async Task CloseCreatePopup(string errorMessage)
        {
            isOpenCreatePopup = false;
            await Task.Run(() =>
            {
                isOpenUpdatePopup = false;
                this.errorMessage = errorMessage;
            });
            if (errorMessage == null)
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
                    Description = errorMessage,
                    NotificationType = NotificationType.Error
                });
            }
        }

        private void CloseDetailsPopup()
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

            if (reply.Students == null)
            {
                errorMessage = reply.Message;
                _ =  Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = errorMessage,
                    NotificationType = NotificationType.Error
                });
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
                total = reply.Count;
                errorMessage = null;
            }
        }

        private async Task HandlePageIndexChange(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            if(!isStudentSearchedFound)
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
        }

        // delete student by id
        private async Task HandleOnDeleteAsync(int id)
        {
            var reply = await StudentService.DeleteAsync(new IdRequest { Id = id });
            if (reply.Success)
            {
                await LoadStudentsAsync();
                if(students != null && students.Count == 1)
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
                errorMessage = reply.Message;
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = errorMessage,
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

        private async Task OnSearchFound(List<StudentProfileModel> studentProfiles)
        {
            await Task.Run(() =>
            {
                students = studentProfiles;
                total = studentProfiles.Count;
                isStudentSearchedFound = true;
            });
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