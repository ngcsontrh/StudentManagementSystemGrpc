using Microsoft.AspNetCore.Components;

namespace Client.Blazor.Components.Layout
{
    public partial class NavMenu
    {
        bool collapsed;

        [Inject]
        NavigationManager Navigation { get; set; } = null!;

        void NavigateToIndex()
        {
            Navigation.NavigateTo(Navigation.Uri, true);
        }

        void NavigateToChart()
        {
            Navigation.NavigateTo("chart");
        }
    }
}
