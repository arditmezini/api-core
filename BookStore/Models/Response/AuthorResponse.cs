namespace BookStore.Models.Response
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AuthorContact AuthorContact { get; set; }
    }

    public class AuthorContact
    {
        public int AuthorId { get; set; }
        public int CountryId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}