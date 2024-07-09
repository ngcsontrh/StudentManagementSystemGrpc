using AutoMapper;
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

        [Inject]
        IMapper Mapper { get; set; } = null!;

        [Parameter]
        public EventCallback<SearchStudentDTO> OnSearch {  get; set; }

        SearchStudentDTO studentFields = new SearchStudentDTO();

        List<ClassInfoDTO>? classes;

        async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfoAsync(new Shared.Empty());
            classes = Mapper.Map<List<ClassInfoDTO>>(reply.Classes);
        }

        async Task HandleOnSearchAsync()
        {
            await OnSearch.InvokeAsync(studentFields);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassesAsync();
        }
    }
}
