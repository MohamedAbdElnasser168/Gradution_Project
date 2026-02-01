namespace Gradution_Project.Models
{
    public class AIRequestLog
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string InputData { get; set; }
        public DateTime RequestTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public AIResponseLog Response { get; set; }
    }
}
