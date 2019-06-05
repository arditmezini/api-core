using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class AuthorConfiguration : BaseConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {

            builder.ToTable("Author");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.AuthorContact)
                .WithOne(x => x.Author)
                .HasForeignKey<AuthorContact>(x => x.AuthorId);
        }
    }
}