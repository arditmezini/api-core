using AspNetCoreApi.Models;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Entities
{
    public partial class BookCategory : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}