using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Shared.Enums
{
    public enum StatusType
    {
        DeActive,
        Active
    }
    public enum RoleType
    {
        User,
        Admin
       
    }
    public enum ContentType
    {
        Blog,
        News
    }
    public enum StatusCodeType
    {
        Ok = 200 ,
        UnAuthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        ServerError = 503
    }
    public enum BannerType
    {
        [Display(Name = "بنر اصلی")]
        IndexMainBanner,
        [Display(Name = "بنر سمت راست صفحه اصلی")]
        IndexSmallRightBanner,
        [Display(Name = "بنر سمت چپ صفحه اصلی")]
        IndexSmallLeftBanner
    }
    public enum SettingKeyType
    {
        Banner,
        Other
    }
}
