using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Comment;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;
using TB.Shared.Enums;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Comment
{
    public partial class CommentList
    {
        #region Properties
        private bool showSpinner;
        private List<CommentDto> comments;
        [Inject]
        private ICommentService _service { get; set; }
        [Inject]
        private IDialogService _dialog { get; set; }
        [Inject]
        private NavigationManager _nav { get; set; }
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Parameter]
        public int Id { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<List<CommentDto>> result = await _service.GetAllComments(Id);

            if (result.Status)
            {
                comments = result.Data;
            }

            await Task.Delay(1000);
            showSpinner = false;
            await base.OnInitializedAsync();
        }
        private async Task ShowConfirmDialog(CommentDto item)
        {
            bool result = (bool)await _dialog.ShowMessageBox("اخطار", "آیا برای حذف مطمئن هستید ؟", "بله", "خیر");

            if (result)
            {
                await Delete(item.Id);
            }
        }
        private async Task ChangeStatus(CommentDto item)
        {
            if (item.Status == StatusType.Active)
            {
                item.Status = StatusType.DeActive;
            }
            else
            {
                item.Status = StatusType.Active;
            }

            showSpinner = true;
            StateHasChanged();

            var response = await _service.Update(item);
            if (response.Status)
            {
                _snackbar.Add(response.Message, Severity.Success);
            }
            else
            {
                _snackbar.Add(response.Message, Severity.Error);
            }

            await Task.Delay(1000);
            showSpinner = false;
            StateHasChanged();
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
                    comments.RemoveAll(p => p.Id == id);
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
        #endregion
    }
}
