using AspNetCoreApi.Models;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Entities
{
    public partial class Author : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual AuthorContact AuthorContact { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}