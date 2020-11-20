using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class NewsConfiguration : BaseConfiguration<News>
    {
        public override void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}