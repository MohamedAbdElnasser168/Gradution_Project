namespace Gradution_Project.Models
{
    public class DailyStat
    {
        public int Id { get; set; }
        public float CurrentWeight { get; set; }
        public float Waist { get; set; }
        public float Chest { get; set; }
        public float BMI { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
