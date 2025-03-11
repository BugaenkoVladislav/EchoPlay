using App.EchoPlay.Fabrics;
using App.EchoPlay.Services;
using Google.Apis.Auth.AspNetCore3;
using Infrastructure.EchoPlay;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace App.EchoPlay;

public static class AppExt
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        //services.AddRepositories();
        services.AddServices();
        services.AddScoped<UnitOfWork>();
        services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = "GoogleScheme";
                o.DefaultSignInScheme = "CookieScheme";
            })
            .AddBearerToken("JwtBearerScheme", options =>
            {
                
            })
            .AddCookie("CookieScheme", options =>
            {
                
            })
            .AddGoogleOpenIdConnect("GoogleScheme",options =>
            {
                //todo get from appsettings.json
                options.ClientId = "clientId";
                options.ClientSecret = "clientSecret";
            });
        //services.AddDbContext<MyDbContext>(options => options.UseNpgsql(connectionString));
        //RolesAuthorization
        services.AddAuthorization();
        services.AddControllers();
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<AuthService>().AddScoped<AuthenticationBuilder>();
    }
}