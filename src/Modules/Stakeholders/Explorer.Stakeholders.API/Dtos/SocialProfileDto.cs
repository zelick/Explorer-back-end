namespace Explorer.Stakeholders.API.Dtos
{
    public class SocialProfileDto
    {
        public int Id { get; set; }
        public List<UserDto> Followers { get; set; }
        public List<UserDto> Followed { get; set; }
    }
}
