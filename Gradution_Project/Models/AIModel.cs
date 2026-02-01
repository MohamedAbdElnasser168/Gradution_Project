namespace Gradution_Project.Models
{
    public class AIModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
