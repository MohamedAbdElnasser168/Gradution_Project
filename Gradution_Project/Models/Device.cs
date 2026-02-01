namespace Gradution_Project.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
