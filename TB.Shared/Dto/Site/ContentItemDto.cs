namespace TB.Shared.Dto.Site
{
    public class ContentItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Whatsapp { get; set; }
        public string? Instagram { get; set; }
        public int UserId { get; set; }
        public int Like { get; set; }
        public int Visit { get; set; }
        public int CategoryId { get; set; }
        public SiteUserDto? User { get; set; }
        public SiteCategoryDto? Category { get; set; }
        public List<CommentItemDto>? Comments { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
