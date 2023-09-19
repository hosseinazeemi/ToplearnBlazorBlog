using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Shared.Dto.Global
{
    public class MediaDto
    {
        public byte[] Bytes { get; set; }
        public string Extension { get; set; } // .jpg
        public string Name { get; set; } // ....
        public string MimeType { get; set; } //image/jpeg
    }
}
