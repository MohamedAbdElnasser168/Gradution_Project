namespace Gradution_Project.Models
{
    public class UserGoal
    {
        public int Id { get; set; }
        public string GoalName { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
