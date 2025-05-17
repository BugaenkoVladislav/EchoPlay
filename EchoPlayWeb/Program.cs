using App.EchoPlay.AddiSettings;
using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Interfaces;
using Google.Apis.Auth.AspNetCore3;
using Infrastructure.EchoPlay.Authentications;

namespace EchoPlayWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var googleSettings = builder.Configuration.GetSection("GoogleSettings").Get<GoogleOpenIdSettings>();
        
        builder.Services.AddHttpContextAccessor(); 
        builder.Services.AddScoped<AuthenticationCreator>();
        
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
                options.DefaultChallengeScheme =GoogleOpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie("CookieScheme", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            }).AddGoogleOpenIdConnect(options =>
            {
                options.ClientId = googleSettings.ClientId;
                options.ClientSecret = googleSettings.ClientSecret;
                options.CallbackPath = "/signin-google";
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