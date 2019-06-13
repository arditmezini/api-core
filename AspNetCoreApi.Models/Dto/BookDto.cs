using System.Collections.Generic;

namespace AspNetCoreApi.Models.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PublishedYear { get; set; }

        public virtual BookCategoryDto BookCategory { get; set; }
        public virtual PublisherDto Publisher { get; set; }
        public virtual ICollection<BookAuthorsDto> BookAuthors { get; set; }
    }
}
