using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class Details : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        StudentDetails? student;
        string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            var reply = await StudentService.GetDetailsAsync(new IdRequest { Id = Id });

            if (reply.Student != null)
            {
                student = reply.Student;
            }
            else
            {
                errorMessage = reply.Message;
            }
        }
    }
}
