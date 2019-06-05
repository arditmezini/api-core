using System.Collections.Generic;

namespace AspNetCoreApi.Models.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public AuthorContactDto AuthorContact { get; set; }
        public ICollection<BookAuthorsDto> BookAuthors { get; set; }
    }
}
