using TB.Domain.Enums;

namespace TB.Domain.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ContentType Type { get; set; }
        public StatusType Status { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Whatsapp { get; set; }
        public string Instagram { get; set; }
        public int Like { get; set; }
        public int Visit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
