using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;
using TB.WebApi.Services;

namespace TB.WebApi.Controllers
{
    [Route("api/content")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContentService _contentService;
        private readonly IFileService _fileService;
        public ContentController(IMapper mapper, IContentService contentService, IFileService fileService)
        {
            _mapper = mapper;
            _contentService = contentService;
            _fileService = fileService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<bool> Create([FromBody] ContentDto data)
        {
            Content model = _mapper.Map<ContentDto, Content>(data);

            try
            {
                if (data.File != null)
                {
                    model.Image = _fileService.Save(data.File, nameof(TB.Domain.Models.Content));
                }
                model.CreatedAt = DateTime.UtcNow;
                model.UpdatedAt = DateTime.UtcNow;

                bool result = _contentService.Create(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("update")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<bool> Update([FromBody] ContentDto data)
        {
            Content model = _mapper.Map<ContentDto, Content>(data);

            try
            {
                if (data.File != null)
                {
                    if (!string.IsNullOrEmpty(data.Image))
                    {
                        _fileService.Delete(data.Image); // Content/dasdas.jpg
                    }
                    model.Image = _fileService.Save(data.File, nameof(TB.Domain.Models.Content));
                }
                model.UpdatedAt = DateTime.UtcNow;

                bool result = _contentService.Update(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<bool> Delete([FromBody] int id)
        {
            try
            {
                bool result = _contentService.Delete(id).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "حذف با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpGet("getAll")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<List<ContentDto>> GetAll()
        {
            try
            {
                List<Content> result = _contentService.GetAll().GetAwaiter().GetResult();
                List<ContentDto> users = _mapper.Map<List<Content>, List<ContentDto>>(result);

                return new ResponseDto<List<ContentDto>>(true, "با موفقیت دریافت شد", users);

            }
            catch (Exception e)
            {
                return new ResponseDto<List<ContentDto>>(false, e.StackTrace, new List<ContentDto>());
            }
        }
        [HttpPost("findById")]
        [Authorize(Roles = "Admin")]
        public ResponseDto<ContentDto> FindById([FromBody] int id)
        {
            try
            {
                Content result = _contentService.FindById(id).GetAwaiter().GetResult();
                ContentDto data = _mapper.Map<Content, ContentDto>(result);

                return new ResponseDto<ContentDto>(true, "با موفقیت دریافت شد", data);

            }
            catch (Exception e)
            {
                return new ResponseDto<ContentDto>(false, e.StackTrace, new ContentDto());
            }
        }
    }
}
