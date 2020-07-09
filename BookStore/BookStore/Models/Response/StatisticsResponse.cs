namespace BookStore.Models.Response
{
    public class StatisticsResponse
    {
        public int BookCount { get; set; }
        public int AuthorCount { get; set; }
        public int CategoryCount { get; set; }
        public int PublisherCount { get; set; }
    }
}