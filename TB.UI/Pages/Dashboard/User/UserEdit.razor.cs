using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.User;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.User
{
    public partial class UserEdit
    {
        #region Properties
        [Parameter]
        public int Id { get; set; }
        private UserDto user = new UserDto();
        private bool showSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IUserService _service { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            showSpinner = true;
            StateHasChanged();

            var response = await _service.FindById(Id);

            if (response.Status)
            {
                if (response.Data != null)
                {
                    user = response.Data;
                }
            }

            showSpinner = false;
            StateHasChanged();
            await base.OnInitializedAsync();
        }
        private async Task Edit()
        {
            // request => save
            showSpinner = true;
            StateHasChanged();

            var response = await _service.Update(user);

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
