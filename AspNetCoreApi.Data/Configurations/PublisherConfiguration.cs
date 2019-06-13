using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class PublisherConfiguration : BaseConfiguration<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}