namespace Gradution_Project.Models
{
    public class RestaurantReview
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }
        public float Rating { get; set; }
        public float SentimentScore { get; set; }
        public string Keywords { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
