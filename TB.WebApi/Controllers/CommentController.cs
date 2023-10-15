using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Comment;
using TB.Shared.Dto.Global;

namespace TB.WebApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpPost("create")]
        public ResponseDto<bool> Create([FromBody] CommentDto data)
        {
            Comment model = _mapper.Map<CommentDto, Comment>(data);

            try
            {
                bool result = _commentService.Create(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("update")]
        public ResponseDto<bool> Update([FromBody] CommentDto data)
        {
            Comment model = _mapper.Map<CommentDto, Comment>(data);

            try
            {
                bool result = _commentService.Update(model).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "ذخیره سازی با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpPost("delete")]
        public ResponseDto<bool> Delete([FromBody] int id)
        {
            try
            {
                bool result = _commentService.Delete(id).GetAwaiter().GetResult();

                return new ResponseDto<bool>(true, "حذف با موفقیت انجام شد", result);
            }
            catch (Exception e)
            {
                return new ResponseDto<bool>(false, e.Message, false);
            }
        }
        [HttpGet("getAllComments")]
        public ResponseDto<List<CommentDto>> GetAllComments([FromQuery]int id)
        {
            try
            {
                List<Comment> result = _commentService.GetAllComments(id).GetAwaiter().GetResult();
                List<CommentDto> comments = _mapper.Map<List<Comment>, List<CommentDto>>(result);

                return new ResponseDto<List<CommentDto>>(true, "با موفقیت دریافت شد", comments);

            }
            catch (Exception e)
            {
                return new ResponseDto<List<CommentDto>>(false, e.StackTrace, new List<CommentDto>());
            }
        }
        [HttpPost("findById")]
        public ResponseDto<CommentDto> FindById([FromBody] int id)
        {
            try
            {
                Comment result = _commentService.FindById(id).GetAwaiter().GetResult();
                CommentDto data = _mapper.Map<Comment, CommentDto>(result);

                return new ResponseDto<CommentDto>(true, "با موفقیت دریافت شد", data);

            }
            catch (Exception e)
            {
                return new ResponseDto<CommentDto>(false, e.StackTrace, new CommentDto());
            }
        }
    }
}
