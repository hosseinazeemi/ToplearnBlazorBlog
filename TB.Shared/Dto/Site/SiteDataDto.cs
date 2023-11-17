using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.Setting;

namespace TB.Shared.Dto.Site
{
    public class SiteDataDto
    {
        public List<CategoryMenuDto>? Categories { get; set; }
        public List<ContentMenuDto>? Blogs { get; set; }
        public List<ContentMenuDto>? News { get; set; }
        public SettingItemDto? Setting{ get; set; }
    }
}
