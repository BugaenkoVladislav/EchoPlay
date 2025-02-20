using App.EchoPlay.Services;
using Grpc.Core;
using UserEntity = Domain.EchoPlay.Entities.User;
using UserDto = User;
using AuthTypeEntity = Domain.EchoPlay.Enums.AuthType;
using AuthTypeDto = AuthType;
namespace AuthenticationGrpcService.Services;

public class AuthGrpcService(AuthService authService):Authenticator.AuthenticatorBase
{
    AuthService _authService = authService;
    public override async Task<Result> Authenticate(AuthenticateData request, ServerCallContext context)
    {
        try
        {
            AuthTypeEntity authType;
            var userData = ConvertUserDtoToUserEntity(request.UserData);
            authType = request.AuthType switch
            {
                AuthTypeDto.Google => AuthTypeEntity.Google,
                AuthTypeDto.Cookie => AuthTypeEntity.Cookie,
                AuthTypeDto.JwtBearer => AuthTypeEntity.JwtBearer,
                _ => throw new Exception("Unknown AuthType")
            };
            await _authService.AuthenticateAsync(userData, authType,request.Code.Code_);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                IsSuccess = false,
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
            IsSuccess = true,
            Code = 200,
            Desc = "Success",
        });
    }

    public override async Task<Result> Unauthenticate(UserDto request, ServerCallContext context)
    {
        try
        {
            var userData = ConvertUserDtoToUserEntity(request);
            await _authService.UnauthenticateAsync(userData);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                IsSuccess = false,
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
            IsSuccess = true,
            Code = 200,
            Desc = "Success",
        });
    }

    public override async Task<Result> Identify(UserDto request, ServerCallContext context)
    {
        try
        {
            var userData = ConvertUserDtoToUserEntity(request);
            await _authService.IdentifyUserAsync(userData);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                IsSuccess = false,
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
            IsSuccess = true,
            Code = 200,
            Desc = "Success",
        });
    }

    private UserEntity ConvertUserDtoToUserEntity(UserDto userData)
    {
        return new UserEntity
        {
            Email = userData.Email,
            Password = userData.Password,
            Phone = userData.Phone,
            Username = userData.Username,
        };
    }
}