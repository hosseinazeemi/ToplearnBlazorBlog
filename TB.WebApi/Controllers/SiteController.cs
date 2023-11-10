using AutoMapper;
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
    }
}
