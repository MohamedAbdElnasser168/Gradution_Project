namespace Gradution_Project.Models
{
    public class AIModelExecution
    {
        public int Id { get; set; }
        public int RunTimeMs { get; set; }

        public int ModelId { get; set; }
        public int RequestId { get; set; }  
    }
}
