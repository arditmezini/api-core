﻿using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Models.Entity.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreApi.Dal.Configurations
{
    public class BookCategoryConfiguration : BaseConfiguration<BookCategory>
    {
        public override void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable("BookCategory", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}