using App.EchoPlay.Services;
using Grpc.Core;
using UserDto = User;
using UserEntity = Domain.EchoPlay.Entities.User;
using AuthTypeEntity = Domain.EchoPlay.Enums.AuthType;
using AuthTypeDto = AuthType;
namespace AuthenticationGrpcService.Services;

public class AuthGrpcService(AuthService authService):Authenticator.AuthenticatorBase
{
    private readonly Dictionary<AuthTypeDto, AuthTypeEntity> _authTypesMapper = new()
    {
        { AuthTypeDto.Google, AuthTypeEntity.Google },
        { AuthTypeDto.Cookie, AuthTypeEntity.Cookie },
        { AuthTypeDto.JwtBearer, AuthTypeEntity.JwtBearer }
    };
    
    private readonly AuthService _authService = authService;
    public override async Task<Result> Authenticate(AuthenticateData request, ServerCallContext context)
    {
        try
        {
            AuthTypeEntity authType;
            var userData = ConvertUserDtoToUserEntity(request.UserData);
            authType = _authTypesMapper[request.AuthType];
            await _authService.AuthenticateAsync(userData, authType,request.Code.Code_);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new Result()
            {
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
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
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
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
                Code = 400,
                Desc = ex.Message,
            });
        }
        return await Task.FromResult(new Result()
        {
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