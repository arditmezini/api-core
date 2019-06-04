using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BookCategoryConfiguration : BaseConfiguration<BookCategory>
    {
        public override void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable("BookCategory");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}