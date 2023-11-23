using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Enums;

namespace TB.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public int ContentId { get; set; }
        public int? UserId { get; set; }
        public StatusType Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User? User { get; set; }
    }
}
