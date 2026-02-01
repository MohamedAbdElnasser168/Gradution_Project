namespace Gradution_Project.Models
{
    public class UserMealPlan
    {
        public int Id { get; set; }
        public string MealType { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
