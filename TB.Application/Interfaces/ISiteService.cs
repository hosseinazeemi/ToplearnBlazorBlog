using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Enums;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface ISiteService
    {
        List<Content> GetLastBlogs();
        List<Content>? GetLastNews();
        Content? GetLastNewsBanner();
        List<Category> GetMenuCategories();
        List<Content> GetMenuContents(ContentType type);
        List<Content> GetPopularNews();
        Setting GetSetting(string key);
        List<Category> GetSpecialCategories();
        List<Content> GetTodayNews();
    }
}
