using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Models.Entity.Configurations
{
    public class AuthorConfiguration : BaseConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {

            builder.ToTable("Author", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.AuthorContact)
                .WithOne(x => x.Author)
                .HasForeignKey<AuthorContact>(x => x.AuthorId)
                .HasConstraintName("FK_AuthorContact_Author");
        }
    }
}