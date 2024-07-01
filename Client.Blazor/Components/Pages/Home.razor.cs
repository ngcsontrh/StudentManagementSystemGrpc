using AntDesign;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProtoBuf.Grpc.Client;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        private IStudentService StudentService { get; set; } = null!;

        [Inject]
        private IClassService ClassService { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation {  get; set; } = null!;

        // models
        private List<StudentProfile>? students;
        private List<ClassInfo>? classes;

        private string? errorMessage;

        private int pageNumber = 1;
        private int pageSize = 10;
        int total = 0; // total students in db

        // grpc datacontract for request
        IdRequest searchId = new IdRequest();

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
            }
            else
            {
                students = reply.Students;
                total = reply.Count;
                errorMessage = null;
            }
        }

        // search student by id
        private async Task SearchById()
        {
            var reply = await StudentService.GetProfileAsync(searchId);
            if(reply.Student == null)
            {
                errorMessage = reply.Message;
                students = null;
                total = 0;
            }
            else
            {
                total = 1;
                students = new List<StudentProfile>()
                {
                    new StudentProfile
                    {
                        Id = reply.Student.Id,
                        FullName = reply.Student.FullName,
                        Birthday = reply.Student.Birthday,
                        Address = reply.Student.Address,
                        ClassId = reply.Student.ClassId,
                        ClassName = reply.Student.ClassName,
                    }
                };
                errorMessage = null;
            }
        }

        private async Task HandlePageIndexChange(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            await LoadStudentsAsync();
        }

        private async Task HandlePageSizeChange(PaginationEventArgs args)
        {
            pageSize = args.PageSize;
            await LoadStudentsAsync();
        }

        // delete student by id
        private async Task HandleOnDeleteAsync(int id)
        {
            var reply = await StudentService.DeleteAsync(new IdRequest { Id = id });
            if (reply.Success)
            {
                await LoadStudentsAsync();
            }
            else
            {
                errorMessage = reply.Message;
            }
        }

        private void NavigateToUpdate(int id)
        {
            Navigation.NavigateTo($"/update/{id}");
        }

        private void NavigateToDetails(int id)
        {
            Navigation.NavigateTo($"/details/{id}");
        }

        private void HandleSortByName()
        {
            students = students!.OrderBy(s => s.FullName).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
        }
    }
}
