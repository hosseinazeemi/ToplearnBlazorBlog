using TB.Shared.Dto.Global;

namespace TB.Shared.Dto.Setting
{
    public class SettingItemDto
    {
        public string? Whatsapp { get; set; }
        public string? Youtube { get; set; }
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? Twitter { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? Rule { get; set; }
        public string? About { get; set; }
        public FileDto? LogoFile { get; set; }
    }
}
