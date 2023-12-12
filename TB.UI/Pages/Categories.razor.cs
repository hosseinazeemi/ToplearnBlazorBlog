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
            await base.OnInitializedAsync();
        }
        private async Task LoadData()
        {
            try
            {
                showSpinner = true;
                StateHasChanged();
                var response = await _service.GetCategory(Id);
                if (response.Status)
                {
                    Category = response.Data;
                }
                await Task.Delay(500);
                showSpinner = false;
                StateHasChanged();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected override async Task OnParametersSetAsync()
        {
            await LoadData();
            await base.OnParametersSetAsync();
        }
    }
}
