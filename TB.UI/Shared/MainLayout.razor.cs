using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Site;
using TB.UI.Services.Site;

namespace TB.UI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        private ISiteService _service { get; set; }
        public SiteDataDto SiteData { get; set; }

        private bool showSite = false;

        protected override async Task OnInitializedAsync()
        {
            ResponseDto<SiteDataDto> data = await _service.GetSiteData();

            if (data.Status)
            {
                SiteData = data.Data;
            }
            await Task.Delay(2000);
            showSite = true;
            StateHasChanged();
            await Js.InvokeVoidAsync("LoadFunctions");
            StateHasChanged();
        }
    }
}
