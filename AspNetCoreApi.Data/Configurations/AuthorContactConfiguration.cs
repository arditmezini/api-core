using AspNetCoreApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class AuthorContactConfiguration : BaseConfiguration<AuthorContact>
    {
        public override void Configure(EntityTypeBuilder<AuthorContact> builder)
        {
            builder.ToTable("AuthorContact");
            builder.HasKey(x => x.AuthorId);
        }
    }
}