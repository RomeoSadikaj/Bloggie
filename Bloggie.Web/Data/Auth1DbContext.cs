using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class Auth1DbContext : IdentityDbContext
    {
        public Auth1DbContext(DbContextOptions<Auth1DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed roles (user, admin, superadmin)
            var userRoleId       = "8e31e1da-f0ea-4147-8bd9-5e4fb782d363";
            var adminRoleId      = "3daa40e4-6a6a-4ce2-ac64-5e9cddfe6fb1";
            var superAdminRoleId = "b33db73d-076d-49f9-9572-7aa82eb3188e";

            var roles = new List<IdentityRole>
            {
                  new IdentityRole
                {
                    Name             = "User",
                    NormalizedName   = "User",
                    Id               = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
                  new IdentityRole
                {
                    Name             = "Admin",
                    NormalizedName   = "Admin",
                    Id               = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                  new IdentityRole
                {
                    Name             = "SuperAdmin",
                    NormalizedName   = "SuperAdmin",
                    Id               = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed superadmin
            var superAdminId = "afec5972-efe4-4f45-b1fd-0074813f5713";

            var superAdminUser = new IdentityUser
            {
                UserName           = "superadmin@bloggie.com",
                Email              = "superadmin@bloggie.com",
                NormalizedEmail    = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id                 = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");


            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to superadminuser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>()
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                },
                new IdentityUserRole<string>()
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId,
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
