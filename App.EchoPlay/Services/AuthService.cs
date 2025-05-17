using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using App.EchoPlay.AddiSettings;
using App.EchoPlay.Dtos;
using App.EchoPlay.Fabrics;
using Domain.EchoPlay.Entities;
using Domain.EchoPlay.Enums;
using Domain.EchoPlay.Interfaces;
using Infrastructure.EchoPlay;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace App.EchoPlay.Services;

public class AuthService(UnitOfWork uow,IEncryption encryption,SMTPSettings smtpSettings)
{
    protected IEncryption _encryption = encryption;
    private UnitOfWork _uow = uow;
    private readonly string _fromEmail = smtpSettings.Email;
    private readonly string _password = smtpSettings.Password;
    private readonly string _smtpServer = smtpSettings.Server;
    

    public async Task IdentifyUserAsync(LoginPasswordDto user)
    {
        try
        {
            //userData.Email = await _encryption.DecryptAsync(userData.Email);
            //userData.Username = await _encryption.DecryptAsync(userData.Username);
            //userData.Password = await _encryption.DecryptAsync(userData.Password);
        
            var userDto = await  GetUserAsync(user.Email, user.Password);
            long code = 0;
            if (userDto.Phone is not null)
            {
                code = await SendCodeOnPhone(userDto);
            }
            else
            {
                code = await SendCodeOnEmail(userDto);
            }
        }
        catch (InvalidOperationException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<User> GetUserAsync(string login, string password)
    {
        return await _uow.UserRepository.GetEntityFirstAsync(x =>
            x.Email == login && x.Password == password|| 
            x.Username == login && x.Password == password);
    } 

    public async Task SignUpAsync(User user,long code)
    { 
        await _uow.UserRepository.AddNewEntityAsync(user);
        await _uow.SaveChangesAsync();
        
        throw new UnauthorizedAccessException();
    }

    public async Task<bool> CheckCorrectCode(User userData, long code)
    {
        try
        {
            await _uow.CodeRepository.GetEntityFirstAsync(x=>Convert.ToInt32(x.Number) == code && x.UserId == userData.Id);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    

    public async Task<long> SendCodeOnEmail(User user)
    {
        var code = GenerateCode();

        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_fromEmail));
        message.To.Add(MailboxAddress.Parse(user.Email));
        message.Subject = "Authentication Code";

        message.Body = new TextPart("plain")
        {
            Text = code.ToString() + " для пользователя " + user.Username
        };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(_smtpServer, 465, true); 
            await smtp.AuthenticateAsync(_fromEmail, _password); 
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка отправки: {ex.Message}");
            throw;
        }

        Console.WriteLine("Mail sent successfully.");
        
        await _uow.CodeRepository.AddNewEntityAsync(new Code()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Number = code.ToString(),
            User = user
        });
        await _uow.SaveChangesAsync();
        return code;
    }



    public async Task<long> SendCodeOnPhone(User user)
    {
        return 0;
    }
    
    private long GenerateCode()
    {
        var rnd = new Random();
        var code = rnd.Next(100000, 999999);
        return code;
    }
}