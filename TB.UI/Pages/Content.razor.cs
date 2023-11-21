﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Comment;
using TB.Shared.Dto.Site;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class Content
    {
        [Inject]
        private ISiteService _service { get; set; }
        [Inject]
        private ISnackbar _toast { get; set; }
        [Parameter]
        public int Id { get; set; }
        private ContentItemDto? ContentDto = new ContentItemDto();
        private CommentItemDto commentDto = new CommentItemDto();
        private bool showSpinner = false;
        private bool showCommentSpinner = false;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await _service.GetContent(Id);
                if (response.Status)
                {
                    ContentDto = response.Data;
                }
                else
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

        private async Task SaveComment()
        {
            showCommentSpinner = true;
            StateHasChanged();
            commentDto.Status = TB.Shared.Enums.StatusType.DeActive;
            commentDto.ContentId = ContentDto.Id;
            try
            {
                var response = await _service.SaveComment(commentDto);
                if (response.Status && response.Data)
                {
                    _toast.Add(response.Message , Severity.Success);
                }else
                {
                    _toast.Add(response.Message, Severity.Error);
                }
                await Task.Delay(1500);
                showCommentSpinner = false;
                StateHasChanged();
            }
            catch (Exception e)
            {
                _toast.Add(e.Message, Severity.Error);
            }
            finally
            {
                commentDto = new CommentItemDto();
            }
        }
    }
}