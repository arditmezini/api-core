using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Models.Entity.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BookConfiguration : BaseConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Isbn)
                .IsRequired()
                .HasMaxLength(13);
            builder.Property(x => x.PublishedYear)
                .IsRequired();

            builder.HasOne(x => x.BookCategory)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("FK_Book_Category");

            builder.HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId)
                .HasConstraintName("FK_Book_Publisher");
        }
    }
}