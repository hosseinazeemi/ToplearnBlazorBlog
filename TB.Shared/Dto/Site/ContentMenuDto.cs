namespace TB.Shared.Dto.Site
{
    public class ContentMenuDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public int UserId { get; set; }
        public SiteUserDto? User  { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
