using System.Collections.Generic;

namespace AspNetCoreApi.Models.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AuthorContactDto AuthorContact { get; set; }
        public ICollection<BookAuthorsDto> BookAuthors { get; set; }
    }
}
