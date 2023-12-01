using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TB.Application.Interfaces;
using TB.Domain.Enums;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.Shared.Dto.Site;
using TB.Shared.Enums;

namespace TB.WebApi.Controllers
{
    [Route("api/site")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        IMapper _mapper;
        ISiteService _service;
        public SiteController(IMapper mapper,ISiteService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("getSiteData")]
        public ResponseDto<SiteDataDto> GetSiteData()
        {
            SiteDataDto data = new SiteDataDto();

            try
            {
                var newsList = _service.GetMenuContents(TB.Domain.Enums.ContentType.News);
                data.News = _mapper.Map<List<Content>, List<ContentMenuDto>>(newsList);

                var blogs = _service.GetMenuContents(TB.Domain.Enums.ContentType.Blog);
                data.Blogs = _mapper.Map<List<Content>, List<ContentMenuDto>>(blogs);

                var categories = _service.GetMenuCategories();
                data.Categories = _mapper.Map<List<Category>, List<CategoryMenuDto>>(categories);

                var setting = _service.GetSetting(TB.Domain.Enums.SettingKeyType.Other.ToString());
                if (setting != null)
                {
                    data.Setting = JsonConvert.DeserializeObject<SettingItemDto>(setting.Value);
                }
                return new ResponseDto<SiteDataDto>(true , "دریافت با موفقیت انجام شد" , data);
            }
            catch (Exception)
            {
                //return new ResponseDto<SiteDataDto>(false, "درخواست با خطا مواجه شد", data);
                throw;
            }
        }

        [HttpGet("getIndexData")]
        public ResponseDto<SiteIndexDataDto> GetIndexData()
        {
            SiteIndexDataDto siteIndexData = new SiteIndexDataDto();
            
            try
            {
                var todayNews = _service.GetTodayNews();
                siteIndexData.TodayNews = _mapper.Map<List<Content>, List<TodayNewsDto>>(todayNews);

                var lastBanner = _service.GetLastNewsBanner();
                siteIndexData.GetLastNewsBanner = _mapper.Map<Content, ContentItemDto>(lastBanner);

                var lastNews = _service.GetLastNews();
                siteIndexData.LastNews = _mapper.Map<List<Content>, List<ContentItemDto>>(lastNews);

                var specialCategories = _service.GetSpecialCategories();
                siteIndexData.SpecialCategories = _mapper.Map<List<Category>, List<SiteCategoryDto>>(specialCategories);

                var lastBlogs = _service.GetLastBlogs();
                siteIndexData.LastBlogs = _mapper.Map<List<Content>, List<ContentItemDto>>(lastBlogs);

                var popularNews = _service.GetPopularNews();
                siteIndexData.PopularNews = _mapper.Map<List<Content>, List<ContentItemDto>>(popularNews);

                var mainBanner = _service.GetSetting(BannerType.IndexMainBanner.ToString());
                if (mainBanner != null)
                {
                    siteIndexData.MainBanner = JsonConvert.DeserializeObject<BannerDto>(mainBanner.Value);
                }

                var leftBanner = _service.GetSetting(BannerType.IndexSmallLeftBanner.ToString());
                if (leftBanner != null)
                {
                    siteIndexData.LeftBanner = JsonConvert.DeserializeObject<BannerDto>(leftBanner.Value);
                }

                var rightBanner = _service.GetSetting(BannerType.IndexSmallRightBanner.ToString());
                if (rightBanner != null)
                {
                    siteIndexData.RightBanner = JsonConvert.DeserializeObject<BannerDto>(rightBanner.Value);
                }
                return new ResponseDto<SiteIndexDataDto>(true , "دریافت با موفقیت انجام شد" , siteIndexData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCategory")]
        public ResponseDto<CategoryPageDto> GetCategory(int id)
        {
            try
            {
                var category = _service.GetCategory(id);

                CategoryPageDto item = _mapper.Map<Category, CategoryPageDto>(category);

                return new ResponseDto<CategoryPageDto>(true  , "دریافت شد" , item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getContent")]
        public ResponseDto<ContentItemDto> GetContent(int id)
        {
            try
            {
                var result = _service.GetContent(id);

                if (result != null)
                {
                    ContentItemDto content = _mapper.Map<Content, ContentItemDto>(result);
                    return new ResponseDto<ContentItemDto>(true, "با موفقیت دریافت شد", content);
                }
                else
                {
                    throw new ArgumentNullException("Get Content" , "Not Found Record");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("saveComment")]
        public ResponseDto<bool> SaveComment(CommentItemDto comment)
        {
            try
            {
                comment.CreatedAt = DateTime.UtcNow;
                var data = _mapper.Map<CommentItemDto, Comment>(comment);

                bool result = _service.SaveComment(data);

                return new ResponseDto<bool>(true , "نظر با موفقیت ثبت شد و پس از تایید مدیریت نمایش داده می شود" , result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("likeContent")]
        public ResponseDto<bool> LikeContent([FromBody]int contentId)
        {
            try
            {
                string? ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();

                bool result = _service.LikeContent(contentId , ip);
                return new ResponseDto<bool>(true , "با موفقیت انجام شد" , result);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("search")]
        public ResponseDto<List<ContentItemDto>> Search(string text)
        {
            try
            {
                var result = _service.Search(text);

                if (result != null)
                {
                    List<ContentItemDto> content = _mapper.Map<List<Content>, List<ContentItemDto>>(result);
                    return new ResponseDto<List<ContentItemDto>>(true, "با موفقیت دریافت شد", content);
                }
                else
                {
                    throw new ArgumentNullException("Search Exception", "Not Found Record");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getSetting")]
        public ResponseDto<SettingDto> GetSetting(string key)
        {
            try
            {
                var result = _service.GetSetting(key);

                SettingDto data = _mapper.Map<Setting, SettingDto>(result);

                return new ResponseDto<SettingDto>(true, "", data);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
