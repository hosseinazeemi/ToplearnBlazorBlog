namespace TB.Domain.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int? ContentId { get; set; }
        public string? IpAddress { get; set; }
    }
}
