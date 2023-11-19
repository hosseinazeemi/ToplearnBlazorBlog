using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Site;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class Categories
    {
        [Inject]
        private ISiteService _service { get; set; }
        [Parameter]
        public int Id { get; set; }
        private CategoryPageDto? Category;
        private bool showSpinner = true;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await _service.GetCategory(Id);
                if (response.Status)
                {
                    Category = response.Data;
                }else
                {
                    // --- 
                }
                await Task.Delay(500);
                showSpinner = false;
                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }
            await base.OnInitializedAsync();
        }
    }
}
