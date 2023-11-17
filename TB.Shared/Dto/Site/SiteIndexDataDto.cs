using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.Setting;

namespace TB.Shared.Dto.Site
{
    public class SiteIndexDataDto
    {
        public List<TodayNewsDto>? TodayNews { get; set; }
        public ContentItemDto? GetLastNewsBanner { get; set; }
        public List<ContentItemDto>? LastNews { get; set; }
        public List<SiteCategoryDto>? SpecialCategories { get; set; }
        public List<ContentItemDto>? LastBlogs { get; set; }
        public List<ContentItemDto>? PopularNews { get; set; }
        public BannerDto? MainBanner { get; set; }
        public BannerDto? RightBanner { get; set; }
        public BannerDto? LeftBanner { get; set; }
    }
}
