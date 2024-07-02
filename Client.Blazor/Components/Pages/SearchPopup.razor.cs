using AntDesign;
using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared;
using Shared.Models;

namespace Client.Blazor.Components.Pages
{
    public partial class SearchPopup : ComponentBase
    {
        string? errorMessage;

        [Parameter]
        public Action OnClose { get; set; } = null!;

        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public List<StudentProfileModel> Students { get; set; } = null!;

        private int pageNumber = 1;
        private int pageSize = 10;

        [Parameter]
        public int TotalStudent { get; set; } // total students in db

        public void ClosePopup()
        {
            OnClose();
            IsVisible = false;
        }

        private void HandlePageIndexChange(PaginationEventArgs args)
        {
            pageNumber = args.Page;
        }

        private void HandlePageSizeChange(PaginationEventArgs args)
        {
            pageSize = args.PageSize;
        }

        protected override void OnInitialized()
        {
            errorMessage = null;
        }
    }
}
