using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.User;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public StatusType Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public UserDto? User { get; set; }
    }
}
