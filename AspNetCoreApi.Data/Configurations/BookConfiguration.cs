using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BookConfiguration : BaseConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.CategoryId)
                .IsRequired();
            builder.Property(x => x.PublisherId)
                .IsRequired();

            builder.HasOne<Publisher>()
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId);
            builder.HasOne<BookCategory>()
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}