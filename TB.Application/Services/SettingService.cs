using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class SettingService:ISettingService
    {
        private IAppDbContext _context;
        public SettingService(IAppDbContext context)
        {
            _context = context;
        }

        public bool UpdateSetting(Setting data)
        {
            try
            {
                var getCurrent = _context.Setting.FirstOrDefault(p => p.Key.Equals(data.Key));

                if (getCurrent != null)
                {
                    getCurrent.Value = data.Value;
                }else
                {
                    _context.Setting.Add(data);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Setting> GetAllSetting()
        {
            try
            {
                List<Setting> items = _context.Setting.ToList();

                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Setting GetSetting(string key)
        {
            try
            {
                Setting data = _context.Setting.FirstOrDefault(p => p.Key.Equals(key));

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
