using Microsoft.AspNetCore.Components;
using MudBlazor;
using TB.Shared.Dto.Site;
using TB.UI.Helper;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class Search
    {
        private string? text;
        [Inject]
        private ISiteService _service { get; set; }
        [Inject]
        private ISnackbar _toast { get; set; }
        private List<ContentItemDto> searchResult;
        protected override async Task OnInitializedAsync()
        {
            text = nav.GetQueryStringByKey<string>(nameof(text));

            try
            {
                var response = await _service.Search(text);
                if (response.Status)
                {
                    await Task.Delay(1500);
                    searchResult = response.Data;
                }
                else
                {
                    _toast.Add(response.Message , Severity.Error);
                }
            }
            catch (Exception e)
            {
                _toast.Add(e.Message, Severity.Error);
            }
            await base.OnInitializedAsync();
        }
    }
}
