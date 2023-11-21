using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Site
{
    public class CommentItemDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عنوان نظر را وارد کنید")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [EmailAddress(ErrorMessage = "ایمیل را بدرستی وارد کنید")]
        public string? Email { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public StatusType Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public SiteUserDto? User { get; set; }
    }
}
