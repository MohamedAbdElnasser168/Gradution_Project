namespace Gradution_Project.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsSent { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
