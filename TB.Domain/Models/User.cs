using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Enums;

namespace TB.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public StatusType Status { get; set; }
        public RoleType Role { get; set; }
        public List<Content> Contents { get; set; }
    }
}
