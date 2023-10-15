using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Category;
using TB.Shared.Dto.Global;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Category
{
    public partial class CategoryCreate
    {
        #region Properties
        private CategoryDto category;
        private bool showSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private ICategoryService _service { get; set; }
        #endregion

        #region Methods
        protected override Task OnInitializedAsync()
        {
            category = new CategoryDto();
            return base.OnInitializedAsync();
        }
        private async Task Create()
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<bool> response = await _service.Create(category);

            if (response.Status)
            {
                var result = response.Data;
                if (result)
                {
                    _snackbar.Add(response.Message, Severity.Success);
                }else
                {
                    _snackbar.Add(response.Message, Severity.Error);
                }
            }

            await Task.Delay(1000);

            showSpinner = false;
            StateHasChanged();
        }
        #endregion
    }
}
