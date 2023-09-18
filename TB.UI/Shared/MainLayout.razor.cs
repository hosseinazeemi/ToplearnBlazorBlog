using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace TB.UI.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Js.InvokeVoidAsync("LoadFunctions");
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
