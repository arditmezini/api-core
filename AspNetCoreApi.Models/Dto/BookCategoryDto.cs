using System.Collections.Generic;

namespace AspNetCoreApi.Models.Dto
{
    public class BookCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookDto> Books { get; set; }
    }
}
