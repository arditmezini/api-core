using System.Collections.Generic;

namespace AspNetCoreApi.Models.Entity
{
    public partial class Author : BaseEntity
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthors>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual AuthorContact AuthorContact { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}