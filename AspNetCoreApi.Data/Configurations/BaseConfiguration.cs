using AspNetCoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.DateCreated)
                .IsRequired();
            builder.Property(x => x.UserCreated)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.UserModified)
                .HasMaxLength(50);
            builder.Property(x => x.IsDeleted)
                .IsRequired();
        }
    }
}
