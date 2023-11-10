﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Application.Interfaces;
using TB.Domain.Enums;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Site;

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
                var newsList = _service.GetMenuContents(ContentType.News);
                data.News = _mapper.Map<List<Content>, List<ContentMenuDto>>(newsList);

                var blogs = _service.GetMenuContents(ContentType.Blog);
                data.Blogs = _mapper.Map<List<Content>, List<ContentMenuDto>>(blogs);

                var categories = _service.GetMenuCategories();
                data.Categories = _mapper.Map<List<Category>, List<CategoryMenuDto>>(categories);

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

                return new ResponseDto<SiteIndexDataDto>(true , "دریافت با موفقیت انجام شد" , siteIndexData);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}