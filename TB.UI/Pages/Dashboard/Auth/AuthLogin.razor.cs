using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Global;
using TB.UI.Services.AuthService;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Auth
{
    public partial class AuthLogin
    {
        #region Properties
        private LoginDto login;
        private bool showSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private IAuthService _service { get; set; }
        [Inject]
        private NavigationManager _nav { get; set; }

        [Inject]
        private IJWTAuthService _authService { get; set; }

        #endregion

        #region Methods
        protected override Task OnInitializedAsync()
        {
            login = new LoginDto();
            return base.OnInitializedAsync();
        }
        private async Task Login()
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<TokenDto> response = await _service.Login(login);

            if (response.Status)
            {
                var result = response.Data;
                if (!string.IsNullOrWhiteSpace(result.Token))
                {
                    await _authService.Login(result.Token);
                    _nav.NavigateTo("/dashboard");
                }
            }else
            {
                Console.WriteLine(response.Data.Token);
                _snackbar.Add(response.Message, Severity.Error);
            }

            await Task.Delay(1000);

            showSpinner = false;
            StateHasChanged();
        }
        #endregion
    }
}
