namespace Gradution_Project.Models
{
    public class RestaurantMenu
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public float Calories { get; set; }
        public bool IsHealthy { get; set; }
        public string Keywords { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
