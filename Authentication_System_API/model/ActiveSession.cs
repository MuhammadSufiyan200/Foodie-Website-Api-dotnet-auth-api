using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_System_API.model
{
    public class ActiveSession
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string? DeviceInfo { get; set; }  
        public string Token { get; set; } 
        public DateTime LoginTime { get; set; }
        public string? IpAddress { get; set; } // New
        public string? Location { get; set; }

        [ForeignKey("UserId")]
        public User? Tuser { get; set; }

    }
}
