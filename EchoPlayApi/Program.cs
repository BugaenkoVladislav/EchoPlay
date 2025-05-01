using App.EchoPlay.Fabrics;
using App.EchoPlay.Services;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;
using Infrastructure.EchoPlay.Authentications;
using Infrastructure.EchoPlay.Encryptions;
using Infrastructure.EchoPlay.Hubs;
using Microsoft.EntityFrameworkCore;

namespace EchoPlayApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Регистрация сервисов
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SecondaryDatabase")));
            builder.Services.AddScoped<AuthenticationCreator>();
            builder.Services.AddRepos();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<AuthService>();

            // Регистрируем зависимость для IAuthentication<User>
            builder.Services.AddScoped<IAuthentication<Domain.EchoPlay.Entities.User>, BaseAuthentication>();
            builder.Services.AddScoped<BaseAuthentication, JwtAuthentication>();     
            builder.Services.AddScoped<BaseAuthentication, CookieAuthentication>();

            // Регистрация других зависимостей
            builder.Services.AddScoped<IEncryption, AESEncryption>();
            // todo builder.Services.AddScoped<IEncryption, RSAEncryption>();

            builder.Services.AddScoped<HttpContextAccessor>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "CookieScheme";
                options.DefaultSignInScheme = "CookieScheme";
            })
            .AddCookie("CookieScheme", options =>
            {
                // Configure cookie authentication
            })
            .AddJwtBearer(options =>
            {
                // Configure JWT authentication
            });

            builder.Services.AddAuthorization(opt =>
            {
                // Настройки авторизации
            });
            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7223") // Разрешаем доступ только с вашего фронтенда
                            .AllowAnyHeader() // Разрешаем любые заголовки
                            .AllowAnyMethod() // Разрешаем любые HTTP-методы
                            .AllowCredentials(); // Разрешаем отправку cookies, если нужно
                    });
            });
            
            builder.Services.AddSignalR(); 

            var app = builder.Build();
            app.UseCors("AllowLocalhost");  
            app.UseHttpsRedirection();
            app.UseHsts();  
            // Настройка маршрутов хабов SignalR
            //app.MapHub<RoomHub>("/roomHub");
            app.MapHub<ChatHub>("/chatHub");
            app.MapHub<StreamingHub>("/streamingHub");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            
            app.Run();
        }
    }
}
