using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Site
{
    public class CategoryPageDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public StatusType Status { get; set; }
        public bool IsSpecial { get; set; }
        public List<ContentItemDto>? Contents { get; set; }
    }
}
