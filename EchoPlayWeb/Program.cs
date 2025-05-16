using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay.Authentications;

namespace EchoPlayWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor(); // ✅ обязательно
        builder.Services.AddScoped<AuthenticationCreator>();
        builder.Services.AddScoped<BaseAuthentication>();
        builder.Services.AddScoped<IAuthentication<User>, JwtAuthentication>();     
        builder.Services.AddScoped<IAuthentication<User>, CookieAuthentication>();

        builder.Services.AddScoped<AuthenticationCreator>();
        // Регистрируем зависимость для IAuthentication<User>
        builder.Services.AddScoped<BaseAuthentication>();
        builder.Services.AddScoped<IAuthentication<User>, JwtAuthentication>();     
        builder.Services.AddScoped<IAuthentication<User>, CookieAuthentication>();

        builder.Services.AddHttpClient("AuthApi", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7222/");
        });
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "CookieScheme";
                options.DefaultSignInScheme = "CookieScheme";
            })
            .AddCookie("CookieScheme", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            })
            .AddJwtBearer(options =>
            {
                // Configure JWT authentication
            });

        builder.Services.AddAuthorization();
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