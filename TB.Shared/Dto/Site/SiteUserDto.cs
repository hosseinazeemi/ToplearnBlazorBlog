using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Site
{
    public class SiteUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Email { get; set; }
        public RoleType Role { get; set; }
    }
}
