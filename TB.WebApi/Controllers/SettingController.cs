using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TB.Application.Interfaces;
using TB.Domain.Models;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.Setting;
using TB.WebApi.Services;

namespace TB.WebApi.Controllers
{
    [Route("api/setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        IFileService _fileService;
        ISettingService _service;
        public SettingController(IFileService fileService , ISettingService service)
        {
            _fileService = fileService;
            _service = service;
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
    }
}
