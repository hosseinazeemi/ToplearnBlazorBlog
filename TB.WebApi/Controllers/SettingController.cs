using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TB.Application.Interfaces;
using TB.Domain.Enums;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.WebApi.Services;

namespace TB.WebApi.Controllers
{
    [Route("api/setting")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SettingController : ControllerBase
    {
        IFileService _fileService;
        ISettingService _service;
        IMapper _mapper;
        public SettingController(IFileService fileService , ISettingService service , IMapper mapper)
        {
            _fileService = fileService;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("banner")]
        public ResponseDto<bool> Banner(BannerDto banner)
        {
            try
            {
                if (banner.File != null)
                {
                    banner.Image = _fileService.Save(banner.File , nameof(TB.Domain.Models.Setting));
                }

                object obj = new
                {
                    Key = banner.Type,
                    Image = banner.Image , 
                    Link = banner.Link ,
                    Type = banner.Type,
                    Title = banner.Title
                };

                Setting setting = new Setting
                {
                    Key = banner.Type,
                    Value = JsonConvert.SerializeObject(obj)
                };
                bool result = _service.UpdateSetting(setting);

                return new ResponseDto<bool>(true , "موفقیت آمیز" , result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("updateSetting")]
        public ResponseDto<bool> UpdateSetting(SettingItemDto data)
        {
            try
            {
                if (data.LogoFile != null)
                {
                    data.Logo = _fileService.Save(data.LogoFile, nameof(TB.Domain.Models.Setting));
                }

                object obj = new
                {
                    Instagram = data.Instagram,
                    Whatsapp = data.Whatsapp,
                    Telegram = data.Telegram,
                    Twitter = data.Twitter,
                    Youtube = data.Youtube,
                    Description = data.Description,
                    Logo = data.Logo,
                    Rule = data.Rule,
                    About = data.About
                };

                Setting setting = new Setting
                {
                    Key = SettingKeyType.Other.ToString(),
                    Value = JsonConvert.SerializeObject(obj)
                };
                bool result = _service.UpdateSetting(setting);

                return new ResponseDto<bool>(true , "موفقیت آمیز" , result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("getSetting")]
        public ResponseDto<List<SettingDto>> GetSetting()
        {
            try
            {
                var result = _service.GetAllSetting();

                List<SettingDto> allSetting = _mapper.Map<List<Setting>, List<SettingDto>>(result);

                return new ResponseDto<List<SettingDto>>(true , "" , allSetting);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("uploadImage")]
        public string UploadImage(IFormFile upload)
        {
            try
            {
                string image  = _fileService.Save(upload , "CKEditor").GetAwaiter().GetResult();

                return JsonConvert.SerializeObject(new
                {
                    uploaded = true , 
                    url = image
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
