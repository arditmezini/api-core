namespace AspNetCoreApi.Models.Dto
{
    public class AuthorContactDto
    {
        public int AuthorId { get; set; }
        public int CountryId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public AuthorDto Author { get; set; }
    }
}
