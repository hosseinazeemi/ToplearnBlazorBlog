using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Site;

namespace TB.UI.Shared.Site.BaseComponents
{
    public partial class Header
    {
        [Parameter]
        public SiteDataDto Data { get; set; }
    }
}
