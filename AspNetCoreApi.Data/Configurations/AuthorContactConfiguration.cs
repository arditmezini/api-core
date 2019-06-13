using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class AuthorContactConfiguration : BaseConfiguration<AuthorContact>
    {
        public override void Configure(EntityTypeBuilder<AuthorContact> builder)
        {
            builder.ToTable("AuthorContact", "dbo");
            builder.HasKey(x => x.AuthorId);

            builder.Property(x => x.CountryId)
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.ContactNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasOne(x => x.Country)
                .WithOne(x => x.AuthorContact)
                .HasForeignKey<Countries>(x => x.Id);
        }
    }
}