using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.Category;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Content
{
    public class ContentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public ContentType Type { get; set; }
        public StatusType Status { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string? Whatsapp { get; set; }
        public string? Instagram { get; set; }
        public int Like { get; set; }
        public int Visit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserDto? User { get; set; }
        public CategoryDto? Category { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public FileDto? File { get; set; }
    }
}
