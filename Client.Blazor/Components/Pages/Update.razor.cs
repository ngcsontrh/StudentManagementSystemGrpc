﻿using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class Update : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        NavigationManager Navigation { get; set; } = null!;

        // models
        StudentForm student = new StudentForm();
        List<ClassInfo>? classes;

        bool success; // use for display alert if update success
        string? errorMessage;

        // update student
        private async Task HandleOnUpdate()
        {
            var reply = await StudentService.UpdateAsync(new UpdateStudentRequest
            {
                Id = student.Id,
                FullName = student.FullName,
                Birthday = student.Birthday,
                Address = student.Address,
                ClassId = student.ClassId
            });
            if (reply.Success)
            {
                success = true;
            }
            else
            {
                errorMessage = reply.Message;
            }
        }

        // load student information to update
        private async Task LoadStudentAsync()
        {
            var reply = await StudentService.GetProfileAsync(new IdRequest { Id = Id});
            if(reply.Student == null)
            {
                errorMessage = reply.Message;
            }
            else
            {
                student.Id = reply.Student.Id;
                student.FullName = reply.Student.FullName;
                student.Birthday = reply.Student.Birthday;
                student.Address = reply.Student.Address;
                student.ClassId = reply.Student.ClassId;
                student.ClassName = reply.Student.ClassName;
            }
        }

        // Load all classes for select class
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
            await LoadStudentAsync();
            await LoadClassesAsync();
        }
    }
}
