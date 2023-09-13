using API.Dtos.UserLink;

namespace API.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public ICollection<LinkDto> UserLinks { get; set; }
    }
}