using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.User
{
    public partial class UserCreate
    {
        #region Properties
        private UserDto user;
        private bool showSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }

        [Inject]
        private IUserService _service { get; set; }
        #endregion

        #region Methods
        protected override Task OnInitializedAsync()
        {
            user = new UserDto();
            return base.OnInitializedAsync();
        }
        private async Task Create()
        {
            showSpinner = true;
            StateHasChanged();
            ResponseDto<bool> response = await _service.Create(user);

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
