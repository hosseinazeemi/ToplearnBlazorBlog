using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.Shared.Enums;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class About
    {
        #region Properties
        [Inject]
        private ISiteService _service { get; set; }
        [Inject]
        private ISnackbar _toast { get; set; }
        SettingItemDto settingData;
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ResponseDto<SettingDto> data = await _service.GetSetting(SettingKeyType.Other.ToString());

                if (data.Status)
                {
                    var desSetting = JsonConvert.DeserializeObject<SettingItemDto>(data.Data.Value);
                    if (desSetting != null)
                    {
                        settingData = desSetting;
                    }
                }
                else
                {
                    _toast.Add(data.Message, Severity.Error);
                }
            }
            catch (Exception e)
            {
                _toast.Add(e.Message, Severity.Error);
            }

            await base.OnInitializedAsync();
        }
        #endregion
    }
}
