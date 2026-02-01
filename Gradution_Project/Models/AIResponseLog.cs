namespace Gradution_Project.Models
{
    public class AIResponseLog
    {
        public int Id { get; set; }
        public string OutputData { get; set; }
        public float Confidence { get; set; }
        public DateTime ResponseTime { get; set; }

        public int RequestId { get; set; }
        public AIRequestLog Request { get; set; }
    }
}
