namespace Authentication_System_API.DTO
{
    public class SocialLoginDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Provider { get; set; } // "Google", "Facebook"
        public string ProviderUserId { get; set; }
        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; }
        public string Location { get; set; }
    }
}
