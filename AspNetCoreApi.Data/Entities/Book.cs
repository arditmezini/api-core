using AspNetCoreApi.Models;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Entities
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthors>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PublishedYear { get; set; }

        public int CategoryId { get; set; }
        public virtual BookCategory BookCategory { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
    }
}