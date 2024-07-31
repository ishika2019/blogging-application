using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myproject.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "59382ea4-6944-4a27-8c01-a3bcf73e4227";
            var writerRoleId = "5cb961a0-b49d-47e6-9230-01ce93774adc";
            var roles = new List<IdentityRole> {

                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name= "Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                }
                ,
                new IdentityRole()
                {
                    Id= writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            var adminRoleId = "de7531c5-8025-4ddd-9eff-397d2673b696";
            var admin = new IdentityUser()
            {
                Id = adminRoleId,
                UserName = "admin@CodeDb.com",
                Email = "admin@CodeDb.com",
                NormalizedEmail = "admin@CodeDb.com".ToUpper(),
                NormalizedUserName = "admin@CodeDb.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin@123");
            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<String>>()
            {

                new()
                {
                    UserId=adminRoleId,
                    RoleId=readerRoleId
                },
                new()
                {
                    UserId=adminRoleId,
                    RoleId=writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<String>>().HasData(adminRoles);




        }
    }
}
