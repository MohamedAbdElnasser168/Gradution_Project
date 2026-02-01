namespace Gradution_Project.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float LocationLat { get; set; }
        public float LocationLong { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public float AveragePrice { get; set; }
        public float HealthScore { get; set; }

        public ICollection<RestaurantMenu> MenuItems { get; set; }
        public ICollection<RestaurantReview> Reviews { get; set; }
    }
}
