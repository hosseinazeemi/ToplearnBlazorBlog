using System;
using System.Collections.Generic;
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
}
