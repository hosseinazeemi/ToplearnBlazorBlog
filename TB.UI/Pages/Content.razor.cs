using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Site;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class Content
    {
        [Inject]
        private ISiteService _service { get; set; }
        [Parameter]
        public int Id { get; set; }
        private ContentItemDto? ContentDto = new ContentItemDto();
        private bool showSpinner = false;
        protected override async Task OnInitializedAsync()
        {
            //try
            //{
            //    var response = await _service.GetContent(Id);
            //    if (response.Status)
            //    {
            //        ContentDto = response.Data;
            //    }
            //    else
            //    {
            //        // --- 
            //    }
            //    await Task.Delay(500);
            //    showSpinner = false;
            //    StateHasChanged();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            await base.OnInitializedAsync();
        }
    }
}
