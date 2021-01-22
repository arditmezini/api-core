namespace AspNetCoreApi.Models.Entity
{
    public partial class AuthorContact : BaseEntity
    {
        public int AuthorId { get; set; }
        public int CountryId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public virtual Author Author { get; set; }
        public virtual Countries Country { get; set; }
    }
}