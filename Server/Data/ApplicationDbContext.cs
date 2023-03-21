using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            CreateSampleRoles(builder);
            CreateSasmpleUsers (builder);
        }

        /*
         The following two identifiers have been created outside the methods because they will be used
         in two places in the "CreateSampleRoles()" method in "IdentityRole", and in "CreateSasmpleUsers()"
         in "IdentityUserRole".
         */
        private string adminRoleGuid = Guid.NewGuid ().ToString ();
        private string userRoleGuid = Guid.NewGuid ().ToString ();
        private void CreateSampleRoles (ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Id = userRoleGuid,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User"
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Id = adminRoleGuid,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "Administrator"
                });
        }


        /*
         The code of the method below is responsible for creating two users in the database
         one with administrative roles and the other with a roll, which has limited capabilities
         in this system.
         First the Id Guid is created, then the password is encoded,
         then the user "administratorUser" is created,
         then an encoded password is assigned to its PasswordHash property,
         then the role "Administrator" is added to this user and on itself
         finally the user is added to the database - the "HasData()" method
         */
        private void CreateSasmpleUsers (ModelBuilder builder)
        {
            string adminGuid = Guid.NewGuid ().ToString ();
            PasswordHasher <IdentityUser> adminPassword = new PasswordHasher<IdentityUser> ();
            var administratorUser = new IdentityUser ()
            {
                Id = adminGuid,
                Email = "admin@admin.pl",
                UserName = "admin@admin.pl",
                NormalizedUserName = "admin@admin.pl".ToUpper(),
                NormalizedEmail = "admin@admin.pl".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            administratorUser.PasswordHash = adminPassword.HashPassword(administratorUser, "SDG%$@5423sdgagSDert");
            
            IdentityUserRole <string> adminRole = new IdentityUserRole<string> ()
            {
                UserId = adminGuid,
                RoleId = adminRoleGuid
            }; 

            string userGuid = Guid.NewGuid ().ToString ();
            PasswordHasher <IdentityUser> userPassword = new PasswordHasher<IdentityUser> ();
            var userUser = new IdentityUser ()
            {
                Id = userGuid,
                Email = "user@user.pl",
                UserName = "user@user.pl",
                NormalizedUserName = "user@user.pl".ToUpper(),
                NormalizedEmail = "user@user.pl".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            userUser.PasswordHash = userPassword.HashPassword(userUser, "SDG%$@5423sdgagSDert");

            IdentityUserRole <string> userRole = new IdentityUserRole<string> ()
            {
                UserId = userGuid,
                RoleId = userRoleGuid
            };


            builder.Entity <IdentityUser> ().HasData (administratorUser, userUser);
            builder.Entity <IdentityUserRole <string>> ().HasData (adminRole, userRole); 
        }

    }
}
