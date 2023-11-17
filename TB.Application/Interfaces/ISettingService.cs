using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface ISettingService
    {
        List<Setting> GetAllSetting();
        Setting GetSetting(string key);
        bool UpdateSetting(Setting data);
    }
}
