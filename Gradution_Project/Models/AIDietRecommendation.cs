namespace Gradution_Project.Models
{
    public class AIDietRecommendation
    {
        public int Id { get; set; }
        public float CaloriesFit { get; set; }
        public float ProteinFit { get; set; }
        public float CarbsFit { get; set; }
        public float FatFit { get; set; }
        public float Score { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int MealId { get; set; }
    }
}
