using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Site;

namespace TB.UI.Shared.Site.BaseComponents
{
    public partial class Footer
    {
        [Parameter]
        public SiteDataDto Data { get; set; }
    }
}
