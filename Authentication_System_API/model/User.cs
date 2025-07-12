using System.ComponentModel.DataAnnotations;

namespace Authentication_System_API.model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        public string? FullName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? PasswordHash { get; set; } 

        //[Required]
        public string? Role { get; set; }

        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public string? Location { get; set; }
        public string? Provider { get; set; }
        public string? ProviderUserId { get; set; }
    }
}
