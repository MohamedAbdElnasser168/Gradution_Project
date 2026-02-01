namespace Gradution_Project.Models
{
    public class AIWorkoutRecommendation
    {
        public int Id { get; set; }
        public float IntensityFit { get; set; }
        public float MuscleFit { get; set; }
        public float GoalFit { get; set; }
        public float Score { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int ExerciseId { get; set; }
    }
}
