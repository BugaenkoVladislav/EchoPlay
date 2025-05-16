using App.EchoPlay.Dtos;
using Domain.EchoPlay.Entities;

namespace App.EchoPlay.Mappers;

public static class UserMappers
{
    public static User MapFromSignUpDto(SignUpDto userData)
    {
        return new User()
        {
            Email = userData.Email,
            Password = userData.Password,
            Username = userData.Username,
            Id = Guid.NewGuid(),
        };
    }

    public static User MapFromLoginPasswordDto(LoginPasswordDto dto)
    {
        return new User()
        {
            Email = dto.Email,
            Password = dto.Password,
        };
    }

    public static User MapFromAuthDto(AuthDto dto)
    {
        return new User()
        {
            Email = dto.UserData.Email,
            Password = dto.UserData.Password,
        };
    }

    public static SignUpDto MapToSignUpDto(User userData)
    {
        return new SignUpDto()
        {
            Email = userData.Email,
            Password = userData.Password,
            Username = userData.Username,
        };
    }

    public static LoginPasswordDto MapToLoginPasswordDto(User userData)
    {
        return new LoginPasswordDto()
        {
            Email = userData.Email,
            Password = userData.Password,
        };
    }

    public static AuthDto MapToAuthDto(User userData)
    {
        return new AuthDto()
        {
            UserData = new LoginPasswordDto()
            {
                Email = userData.Email,
                Password = userData.Password,
            }
        };
    }
}