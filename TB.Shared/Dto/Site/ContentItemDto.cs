namespace TB.Shared.Dto.Site
{
    public class ContentItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public int CatgoryId { get; set; }
        public SiteUserDto? User { get; set; }
        public SiteCategoryDto? Category { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
