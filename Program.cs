using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Areas.Admin.Repository.BlogPostRepo;
using PersonalBlog.Areas.Admin.Repository.CommentRepo;
using PersonalBlog.Areas.Identity.Data;
using PersonalBlog.Data;
using PersonalBlog.Repository.CommentRepo;

namespace PersonalBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IBlogPostRepo, BlogPostRepoImpl>();
            builder.Services.AddScoped<ICommentRepo, CommentRepoImpl>();
            builder.Services.AddDefaultIdentity<PersonalBlogUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>();
           
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDbContextConnection")));

            builder.Services.AddAuthentication()
               .AddGoogle(options =>
               {
                   IConfigurationSection googleAuthNSection =
                   builder.Configuration.GetSection("Authentication:Google");
                   options.ClientId = googleAuthNSection["ClientId"];
                   options.ClientSecret = googleAuthNSection["ClientSecret"];
               });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapRazorPages();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Map routes for the Admin area
            app.MapAreaControllerRoute(
                name: "admin",
                areaName: "Admin",
                pattern: "admin/{controller=Home}/{action=Index}/{id?}");
            // Map routes for the Admin area
            app.MapAreaControllerRoute(
                name: "identity",
                areaName: "Identity",
                pattern: "identity/{controller=Account}/{action=Index}/{id?}");


            // Map routes for the client side (outside the Admin area)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}