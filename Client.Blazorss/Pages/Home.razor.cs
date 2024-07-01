/*using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using ProtoBuf.Grpc.Client;
using Shared;

namespace Client.Blazor.Pages
{
    public partial class Home
    {
        [Inject]
        public IStudentService StudentService { get; set; }
        private List<StudentProfile> students;

        protected override async Task OnInitializedAsync()
        {
            await GetStudent();
        }
        private async Task GetStudent()
        {
            var reply = await StudentService.GetAllProfilesAsync(new Empty());
            students = reply.Students;
        }
    }
}
*/