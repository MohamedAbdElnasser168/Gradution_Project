namespace Gradution_Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Allergies { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ActivityLevelId { get; set; }
        public ActivityLevel ActivityLevel { get; set; }

        public int GoalId { get; set; }
        public UserGoal Goal { get; set; }

        public ICollection<DailyStat> DailyStats { get; set; }
        public ICollection<UserMealPlan> MealPlans { get; set; }
        public ICollection<UserWorkoutPlan> WorkoutPlans { get; set; }
    }
}
