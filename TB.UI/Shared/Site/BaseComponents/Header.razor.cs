using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Site;

namespace TB.UI.Shared.Site.BaseComponents
{
    public partial class Header
    {
        [Inject]
        private NavigationManager nav { get; set; }
        [Parameter]
        public SiteDataDto Data { get; set; }

        private SearchDto search = new SearchDto();
        private void Search()
        {
            var parameters = new Dictionary<string, string>
            {
                ["text"] = search.Text
            };

            nav.NavigateTo(QueryHelpers.AddQueryString("/search", parameters));
        }
    }
}
