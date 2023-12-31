﻿using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.Shared.Dto.Site;

namespace TB.UI.Services.Site
{
    public class SiteService:ISiteService
    {
        public IHttpClientService _http { get; set; }
        string baseUrl = "/api/site";
        public SiteService(IHttpClientService http)
        {
            _http = http;
        }

        public async Task<ResponseDto<SiteDataDto>> GetSiteData()
        {
            return await _http.GetAsync<SiteDataDto>($"{baseUrl}/getSiteData");
        }
        public async Task<ResponseDto<SiteIndexDataDto>> GetIndexData()
        {
            return await _http.GetAsync<SiteIndexDataDto>($"{baseUrl}/getIndexData");
        }
        public async Task<ResponseDto<CategoryPageDto>> GetCategory(int id)
        {
            return await _http.GetAsync<CategoryPageDto>($"{baseUrl}/getCategory?id={id}");
        }
        public async Task<ResponseDto<ContentItemDto>> GetContent(int id)
        {
            return await _http.GetAsync<ContentItemDto>($"{baseUrl}/getContent?id={id}");
        }
        public async Task<ResponseDto<bool>> SaveComment(CommentItemDto comment)
        {
            return await _http.PostAsync<bool , CommentItemDto>($"{baseUrl}/saveComment" , comment);
        }
        public async Task<ResponseDto<bool>> LikeContent(int contentId)
        {
            return await _http.PostAsync<bool , int>($"{baseUrl}/likeContent" , contentId);
        }
        public async Task<ResponseDto<List<ContentItemDto>>> Search(string text)
        {
            return await _http.GetAsync<List<ContentItemDto>>($"{baseUrl}/search?text={text}");
        }
        public async Task<ResponseDto<SettingDto>> GetSetting(string key)
        {
            return await _http.GetAsync<SettingDto>($"{baseUrl}/getSetting?key={key}");
        }
    }

    public interface ISiteService
    {
        Task<ResponseDto<CategoryPageDto>> GetCategory(int id);
        Task<ResponseDto<ContentItemDto>> GetContent(int id);
        Task<ResponseDto<SiteIndexDataDto>> GetIndexData();
        Task<ResponseDto<SettingDto>> GetSetting(string key);
        Task<ResponseDto<SiteDataDto>> GetSiteData();
        Task<ResponseDto<bool>> LikeContent(int contentId);
        Task<ResponseDto<bool>> SaveComment(CommentItemDto comment);
        Task<ResponseDto<List<ContentItemDto>>> Search(string text);
    }
}
