using e_commerce_website.Extension;
using e_commerce_website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Data
{
    public class ShopDbContext : IdentityDbContext<AppUser,AppRole, Guid>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Provider> providers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Evaluation> evaluations { get; set; }
        public DbSet<Reply> replies { get; set; }
        public DbSet<Chat> chats { get; set; }
        public static ShopDbContext Create(DbContextOptions<ShopDbContext> options)
        {
            return new ShopDbContext(options);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            builder.Seed();
            //builder.Entity<IdentityRole>().HasKey<Guid>(r => r.Id).ToTable("AppRoles");
            //builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            //builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            //builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
        }
    }
}
