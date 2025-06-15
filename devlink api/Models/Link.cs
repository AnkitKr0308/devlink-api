namespace devlink_api.Models
{
    public class Link
    {
        public int id { get; set; }
        public required string Title { get; set; }
        public required string Url { get; set; }
        public  string Category { get; set; }
        public required string Description { get; set; }
        public required DateTime Date { get; set; }
        public required string UserId { get; set; }

    }
}
