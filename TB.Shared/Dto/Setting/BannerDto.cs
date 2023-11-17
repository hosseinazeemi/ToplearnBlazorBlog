using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.Global;

namespace TB.Shared.Dto.Setting
{
    public class BannerDto
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public FileDto? File { get; set; }
    }
}
