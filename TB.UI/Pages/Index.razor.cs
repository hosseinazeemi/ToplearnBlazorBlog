using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Site;
using TB.UI.Services.Site;

namespace TB.UI.Pages
{
    public partial class Index
    {
        [Inject]
        private ISiteService _service { get; set; }
        private SiteIndexDataDto IndexData;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await _service.GetIndexData();
                if (response.Status)
                {
                    IndexData = response.Data;
                }
            }
            catch (Exception)
            {

                throw;
            }
            await base.OnInitializedAsync();
        }
    }
}
