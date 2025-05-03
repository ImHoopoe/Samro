using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

public class EncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(IConfiguration config)
    {
        string encryptionKeyBase64 = config["JwtSettings:SecretKey"]
                                     ?? throw new ArgumentNullException("JwtSettings:SecretKey");
        _key = Convert.FromBase64String(encryptionKeyBase64);

        if (!(_key.Length == 16 || _key.Length == 24 || _key.Length == 32))
        {
            throw new CryptographicException(
                $"طول کلید برای AES باید 128، 192 یا 256 بیت باشد. طول فعلی کلید {_key.Length * 8} بیت است.");
        }
    }

    public string Encrypt(string text)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();
        var iv = aes.IV;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var plainBytes = Encoding.UTF8.GetBytes(text);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var result = new byte[iv.Length + encryptedBytes.Length];
        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        var fullCipher = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = _key;
        int ivLength = aes.BlockSize / 8; 
        var iv = new byte[ivLength];
        Buffer.BlockCopy(fullCipher, 0, iv, 0, ivLength);
        aes.IV = iv;
        var cipherBytes = new byte[fullCipher.Length - ivLength];
        Buffer.BlockCopy(fullCipher, ivLength, cipherBytes, 0, cipherBytes.Length);

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
