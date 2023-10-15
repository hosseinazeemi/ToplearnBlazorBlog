using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Content
{
    public partial class ContentCreate
    {
        #region Properties
        private ContentDto content;
        private bool showSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }

        [Inject]
        private IContentService _service { get; set; }
        #endregion

        #region Methods
        protected override Task OnInitializedAsync()
        {
            content = new ContentDto();
            return base.OnInitializedAsync();
        }
        private async Task Create()
        {
            showSpinner = true;
            StateHasChanged();
            content.UserId = 2;
            ResponseDto<bool> response = await _service.Create(content);

            if (response.Status)
            {
                var result = response.Data;
                if (result)
                {
                    _snackbar.Add(response.Message, Severity.Success);
                }
                else
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
