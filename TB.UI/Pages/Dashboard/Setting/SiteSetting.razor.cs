﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Setting
{
    public partial class SiteSetting
    {
        #region Properties
        private BannerDto banner;
        private SettingItemDto setting;
        private bool showSpinner;
        private bool showSettingSpinner;
        [Inject]
        private ISnackbar _snackbar { get; set; }
        [Inject]
        private ISettingService _service { get; set; }
        #endregion

        #region Methods
        protected override Task OnInitializedAsync()
        {
            banner = new BannerDto();
            setting = new SettingItemDto();
            
            return base.OnInitializedAsync();
        }
        private async Task SubmitSetting()
        {
            showSettingSpinner = true;
            StateHasChanged();

            ResponseDto<bool> response = await _service.UpdateSetting(setting);

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

            showSettingSpinner = false;
            StateHasChanged();
        }
        private async Task Submit()
        {
            showSpinner = true;
            StateHasChanged();

            ResponseDto<bool> response = await _service.SetBanner(banner);

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
        private void ConfirmFile(FileDto file)
        {
            banner.File = file;
        }
        private void ConfirmLogoFile(FileDto file)
        {
            setting.LogoFile = file;
        }
        #endregion
    }
}