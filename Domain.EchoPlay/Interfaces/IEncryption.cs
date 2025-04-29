namespace Domain.EchoPlay.Interfaces;

public interface IEncryption
{
    /// <summary>
    /// Шифрование данных
    /// </summary>
    /// <param name="decryptedValue"></param>
    /// <returns>возвращает зашифрованную строку</returns>
    Task<string> EncryptAsync(string decryptedValue);
    /// <summary>
    /// Дешифрование данных 
    /// </summary>
    /// <param name="encryptedData"></param>
    /// <returns>возвращает расшифрованную строку</returns>
    Task<string> DecryptAsync(string encryptedData);
}