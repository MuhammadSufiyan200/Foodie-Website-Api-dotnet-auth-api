using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication_System_API.model
{
    public class SystemLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Action { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? LogLevel { get; set; }
        public string? IPAddress { get; set; }

        [ForeignKey("UserId")]
        public User? Tuser { get; set; }

    }
}
