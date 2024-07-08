using Client.Blazor.DTOs;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class SearchField : ComponentBase
    {
        [Inject]
        IStudentService StudentService { get; set; } = null!;

        [Inject]
        IClassService ClassService { get; set; } = null!;

        [Parameter]
        public EventCallback<SearchStudentDTO> OnSearch {  get; set; }

        SearchStudentDTO studentFields = new SearchStudentDTO();

        List<ClassInfoDTO>? classes;

        private async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfo(new Shared.Empty());
            classes = reply.Classes!.Select(c => new ClassInfoDTO
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
            }).ToList();
        }

        private async Task HandleOnSearchAsync()
        {
            await OnSearch.InvokeAsync(studentFields);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}
