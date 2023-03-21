using Blog.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Blog.Server
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            /*
             The options below specify how strong the password should be for the user who registers in the system.
              The first option tells whether the user should receive an e-mail to be able to confirm his authenticity,
             if he confirms it then the account will be authorized.
             Here you can also set how many attempts the user has to enter the wrong password before the account
             will be bookmarked, in this case it has three tries.
            */
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays (2);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /*
                Authentication with JWT options says that the tokens that are exchanged between the client and the server
                 will be encrypted, this slightly increases the security level of the application because it cannot be openly
             read this data. The token between the server and the client is exchanged at the time of login
             user to the system. The token contains data about e.g. email or role.
             A symmetric key often used in encryption is used to encrypt the data
             this data.
            */
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:SecurityKey"])),
                        IgnoreTrailingSlashWhenValidatingAudience = true,  
                        RequireExpirationTime = true,
                        RequireAudience = true,
                        RequireSignedTokens = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = Configuration["JWT:Issuer"],
                        ValidIssuer = Configuration["JWT:Issuer"]
                    };
                });

            /*
            Authorization allows you to add roles to users.
             This was done because authorization to individual subpages takes place
             based on the identification of the user's role. Authorizations are individual
             subpages, the "AuthorizeView" component is used for authorizations
            */
            services.AddAuthorization();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            /*
            Adding the following two lines of code means that there is a login in the system.
             Authentication gives you the ability to log in and checks if the user is in the database,
             while authorization is responsible for which part of the system or page a given user is assigned to
             can have access, i.e. "Administrator" can view all pages
             and "User" only has access to certain components, for example, cannot create new posts.
             */
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
