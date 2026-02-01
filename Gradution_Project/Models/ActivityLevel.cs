namespace Gradution_Project.Models
{
    public class ActivityLevel
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public float Multiplier { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
