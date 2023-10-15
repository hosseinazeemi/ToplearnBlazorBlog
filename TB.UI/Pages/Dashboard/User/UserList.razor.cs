using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.User
{
    public partial class UserList
    {
        #region Properties
        private bool showSpinner;
        private List<UserDto> users;
        [Inject]
        private IUserService _service { get; set; }
        [Inject]
        private IDialogService _dialog { get; set; }
        [Inject]
        private NavigationManager _nav { get; set; }
        [Inject]
        private ISnackbar _snackbar { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<List<UserDto>> result = await _service.GetAll();

            if (result.Status)
            {
                users = result.Data;
            }

            await Task.Delay(1000);
            showSpinner = false;
            await base.OnInitializedAsync();
        }
        private async Task ShowConfirmDialog(UserDto item)
        {
            bool result = (bool)await _dialog.ShowMessageBox("اخطار", "آیا برای حذف مطمئن هستید ؟", "بله", "خیر");

            if (result)
            {
                await Delete(item.Id);
            }
        }
        private async Task Delete(int id)
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<bool> response = await _service.Delete(id);

            if (response.Status)
            {
                if (response.Data)
                {
                    _snackbar.Add(response.Message, Severity.Success);
                    users.RemoveAll(p => p.Id == id);
                }
                else
                {
                    _snackbar.Add(response.Message, Severity.Error);
                }
            }

            await Task.Delay(300);
            showSpinner = false;
            StateHasChanged();
        }
        private void Edit(int id)
        {
            _nav.NavigateTo($"/dashboard/users/edit/{id}");
        }
        #endregion
    }
}
