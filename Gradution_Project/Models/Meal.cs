namespace Gradution_Project.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Carbs { get; set; }
        public float Fat { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<UserMealPlan> UserMealPlans { get; set; }
    }
}
