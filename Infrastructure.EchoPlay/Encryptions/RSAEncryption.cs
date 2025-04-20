using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Encryptions;

public class RSAEncryption:IEncryption
{
    public Task<string> EncryptAsync(string decryptedValue)
    {
        throw new NotImplementedException();
    }

    public Task<string> DecryptAsync(string encryptedData)
    {
        throw new NotImplementedException();
    }
}