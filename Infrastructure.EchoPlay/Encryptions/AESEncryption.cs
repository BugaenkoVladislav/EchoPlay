using System.Security.Cryptography;
using System.Text;
using Domain.EchoPlay.Interfaces;

namespace Infrastructure.EchoPlay.Encryptions;

public class AESEncryption : IEncryption
{
    private string key = "12345678901234567890123456789012";

    /// <summary>
    /// каждый раз генерирует рандомный IV что-бы не ключ не повторялся
    /// </summary>
    /// <returns></returns>
    private static byte[] GenerateRandomIV()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] iv = new byte[16]; // Для AES IV должен быть 16 байт (128 бит)
        rng.GetBytes(iv);
        return iv;
    }


    public async Task<string> EncryptAsync(string decryptedValue)
    {
        
        return await Task.Run(() =>
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = GenerateRandomIV(); 

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(decryptedValue);
            }
            byte[] encryptedData = msEncrypt.ToArray();
            
            // Возвращаем IV и зашифрованные данные в виде строки (IV передается вместе с зашифрованными данными)
            byte[] result = new byte[aesAlg.IV.Length + encryptedData.Length];
            Array.Copy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
            Array.Copy(encryptedData, 0, result, aesAlg.IV.Length, encryptedData.Length);

            return Convert.ToBase64String(result);
        });
    }

    public async Task<string> DecryptAsync(string encryptedData)
    {
        return await Task.Run(() =>
        {
            var encryptedWithIvBytes = Convert.FromBase64String(encryptedData);
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            byte[] iv = new byte[16];
            Array.Copy(encryptedWithIvBytes, 0, iv, 0, iv.Length);
            aesAlg.IV = iv;
            
            // Извлекаем зашифрованные данные (после IV)
            byte[] encryptedBytes = new byte[encryptedWithIvBytes.Length - iv.Length];
            Array.Copy(encryptedWithIvBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);
            
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream msDecrypt = new MemoryStream(encryptedBytes);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            string decrypted = srDecrypt.ReadToEnd();  // Дешифруем данные
            return decrypted;
        });
    }
}