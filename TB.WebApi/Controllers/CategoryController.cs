using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Category;
using TB.Shared.Dto.Global;
using TB.WebApi.Services;

namespace TB.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        public CategoryController(IMapper mapper, ICategoryService categoryService,IFileService fileService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _fileService = fileService;
        }

        [HttpPost("create")]
        public ResponseDto<bool> Create([FromBody] CategoryDto data)
        {
            Category model = _mapper.Map<CategoryDto, Category>(data);

            try
            {
                if (data.File != null)
                {
                    model.Image = _fileService.Save(data.File, nameof(TB.Domain.Models.Category));
                }

                bool result = _categoryService.Create(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("update")]
        public ResponseDto<bool> Update([FromBody] CategoryDto data)
        {
            Category model = _mapper.Map<CategoryDto, Category>(data);

            try
            {
                if (data.File != null)
                {
                    if (!string.IsNullOrEmpty(data.Image))
                    {
                        _fileService.Delete(data.Image);
                    }
                    model.Image = _fileService.Save(data.File, nameof(TB.Domain.Models.Category));
                }

                bool result = _categoryService.Update(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("delete")]
        public ResponseDto<bool> Delete([FromBody]int id)
        {
            try
            {
                bool result = _categoryService.Delete(id).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "حذف با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpGet("getAll")]
        public ResponseDto<List<CategoryDto>> GetAll()
        {
            try
            {
                List<Category> result = _categoryService.GetAll().GetAwaiter().GetResult();
                List<CategoryDto> categories = _mapper.Map<List<Category>, List<CategoryDto>>(result);

                return new ResponseDto<List<CategoryDto>>(true, "با موفقیت دریافت شد", categories);

            }
            catch (Exception e)
            {
                return new ResponseDto<List<CategoryDto>>(false, e.StackTrace, new List<CategoryDto>());
            }
        }
        [HttpPost("findById")]
        public ResponseDto<CategoryDto> FindById([FromBody]int id)
        {
            try
            {
                Category result = _categoryService.FindById(id).GetAwaiter().GetResult();
                CategoryDto data = _mapper.Map<Category, CategoryDto>(result);

                return new ResponseDto<CategoryDto>(true, "با موفقیت دریافت شد", data);

            }
            catch (Exception e)
            {
                return new ResponseDto<CategoryDto>(false, e.StackTrace, new CategoryDto());
            }
        }
    }
}
