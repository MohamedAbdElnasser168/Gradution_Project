namespace Gradution_Project.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
        public int DurationInMin { get; set; }
        public float CaloriesBurned { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public ICollection<UserWorkoutPlan> UserWorkoutPlans { get; set; }
    }
}
