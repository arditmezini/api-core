using AspNetCoreApi.Models;

namespace AspNetCoreApi.Dal.Entities
{
    public partial class AuthorContact : BaseEntity
    {
        public int AuthorId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public virtual Author Author { get; set; }
    }
}