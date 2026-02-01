namespace Gradution_Project.Models
{
    public class UserWorkoutPlan
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
