using AspNetCoreApi.Dal.Configurations;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models;
using AspNetCoreApi.Models.Common.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreApi.Data.DataContext
{
    public class ApiContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiContext(DbContextOptions<ApiContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        #region DbSets

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorContact> AuthorContacts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthors> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AddEntityConfigurations(builder);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        private void AddEntityConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new AuthorContactConfiguration());
            builder.ApplyConfiguration(new BookCategoryConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new PublisherConfiguration());
            builder.ApplyConfiguration(new BookAuthorsConfiguration());
        }

        private void OnBeforeSaving()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity &&
                    (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            var username = httpContextAccessor?.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                    ((BaseEntity)entity.Entity).UserCreated = username;
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).DateModified = DateTime.Now;
                    ((BaseEntity)entity.Entity).UserModified = username;
                }
                else if (entity.State == EntityState.Deleted)
                {
                    //disable delete from database
                    return;
                }
            }
        }
    }
}