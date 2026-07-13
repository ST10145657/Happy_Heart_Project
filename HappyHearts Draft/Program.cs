using HappyHearts_Draft.Services;
using HappyHearts_Draft.Models;
using HappyHearts_Draft.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace HappyHearts_Draft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";

        options.Cookie.Name = "HappyHeartsAuth";
    });


            // Register Services
            builder.Services.AddSingleton<ISupabaseService, SupabaseService>();

            builder.Services.Configure<SupabaseSettings>(
            builder.Configuration.GetSection("Supabase"));
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<PetService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<NewsletterService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
