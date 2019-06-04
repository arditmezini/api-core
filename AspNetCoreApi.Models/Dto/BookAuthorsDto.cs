namespace AspNetCoreApi.Models.Dto
{
    public class BookAuthorsDto
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public virtual AuthorDto Author { get; set; }
        public virtual BookDto Book { get; set; }
    }
}
