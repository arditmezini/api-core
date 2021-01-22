using System.Collections.Generic;

namespace AspNetCoreApi.Models.Entity
{
    public partial class BookCategory : BaseEntity
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}