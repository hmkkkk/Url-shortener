namespace API.Dtos.UserLink
{
    public class LinkDto
    {
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public string? DisplayName { get; set; }
    }
}