using TB.Shared.Dto.Comment;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;

namespace TB.UI.Services.Repository
{
    public class CommentService : ICommentService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/comments";
        public CommentService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<bool>> Create(CommentDto data)
        {
            return await _http.PostAsync<bool, CommentDto>($"{baseUrl}/create", data);
        }
        public async Task<ResponseDto<bool>> Update(CommentDto data)
        {
            return await _http.PostAsync<bool, CommentDto>($"{baseUrl}/update", data);
        }
        public async Task<ResponseDto<List<CommentDto>>> GetAllComments(int contentId = 0)
        {
            return await _http.GetAsync<List<CommentDto>>($"{baseUrl}/getAllComments?id={contentId}");
        }
        //public async Task<ResponseDto<List<CommentDto>>> GetAllByContentId(int id)
        //{
        //    return await _http.GetAsync<List<CommentDto>>($"{baseUrl}/getAll");
        //}
        public async Task<ResponseDto<CommentDto>> FindById(int id)
        {
            return await _http.PostAsync<CommentDto, int>($"{baseUrl}/findById", id);
        }
        public async Task<ResponseDto<bool>> Delete(int id)
        {
            return await _http.PostAsync<bool, int>($"{baseUrl}/delete", id);
        }
    }
    public interface ICommentService
    {
        Task<ResponseDto<bool>> Create(CommentDto data);
        Task<ResponseDto<bool>> Delete(int id);
        Task<ResponseDto<CommentDto>> FindById(int id);
        Task<ResponseDto<List<CommentDto>>> GetAllComments(int contentId = 0);
        Task<ResponseDto<bool>> Update(CommentDto data);
    }
}
