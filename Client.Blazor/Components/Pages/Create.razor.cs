using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Client.Blazor.Components.Pages
{
    public partial class Create : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        NavigationManager Navigation { get; set; } = null!;

        StudentForm student = new StudentForm();
        string? errorMessage;
        List<ClassInfo>? classes;

        private async Task HandleOnCreateAsync()
        {
            var reply = await StudentService.CreateAsync(new CreateStudentRequest
            {
                FullName = student.FullName,
                Birthday = student.Birthday,
                Address = student.Address,
                ClassId = student.ClassId
            });
            if(reply.Success)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                errorMessage = reply.Message;
            }
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
                classes = reply.Classes;
            }
        
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}
