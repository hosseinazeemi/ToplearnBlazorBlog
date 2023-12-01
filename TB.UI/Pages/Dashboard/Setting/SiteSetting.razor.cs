using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.Shared.Enums;
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
        private List<BannerDto> allBanners = new List<BannerDto>();
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            banner = new BannerDto();
            setting = new SettingItemDto();

            try
            {
                var response = await _service.GetSetting();

                if (response.Status)
                {
                    var settingItem = response.Data.FirstOrDefault(p => p.Key.Equals(SettingKeyType.Other.ToString()));
                    if (settingItem != null)
                    {
                        var desSetting = JsonConvert.DeserializeObject<SettingItemDto>(settingItem.Value);
                        if (desSetting != null)
                        {
                            setting = desSetting;
                        }
                    }

                    var banners = response.Data
                        .Where(p => p.Key.Equals(BannerType.IndexMainBanner.ToString()) || p.Key.Equals(BannerType.IndexSmallRightBanner.ToString()) || p.Key.Equals(BannerType.IndexSmallLeftBanner.ToString())).ToList();

                    foreach (var item in banners)
                    {
                        if (item?.Value != null)
                        {
                            var desItem = JsonConvert.DeserializeObject<BannerDto>(item.Value);
                            allBanners.Add(desItem);
                        }
                    }
                }
                else
                {
                    _snackbar.Add(response.Message, Severity.Error);
                }
            }
            catch (Exception e)
            {
                _snackbar.Add(e.Message, Severity.Error);

            }
            await base.OnInitializedAsync();
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
