using AspNetCoreApi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthors>
    {
        public void Configure(EntityTypeBuilder<BookAuthors> builder)
        {
            builder.ToTable("BookAuthors", "dbo");
            builder.HasKey(x => new
            {
                x.BookId,
                x.AuthorId
            });

            builder.HasOne<Book>()
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.BookId);
            builder.HasOne<Author>()
                .WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.AuthorId);
        }
    }
}
