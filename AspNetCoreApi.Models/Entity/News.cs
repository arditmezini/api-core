namespace AspNetCoreApi.Models.Entity
{
    public partial class News : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}