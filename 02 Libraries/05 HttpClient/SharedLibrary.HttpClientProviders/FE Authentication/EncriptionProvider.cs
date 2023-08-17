using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.HttpClientProviders;

public class EncriptionProvider : IEncriptionProvider
{
    #region [ Fields ]
    public readonly IConfiguration _configuration;
    public readonly string _key;
    #endregion

    #region [ CTor ]
    public EncriptionProvider(IConfiguration _configuration) {
        this._configuration = _configuration;
        this._key = this._configuration["EncryptionKey"];
    }
    #endregion

    public string Encrypt(string text) {
        var result = string.Empty;

        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = iv;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream()) {
                using (var cryptoStream = new CryptoStream((Stream) ms, encryptor, CryptoStreamMode.Write)) {
                    using (var streamWriter = new StreamWriter((Stream) cryptoStream)) {
                        streamWriter.Write(text);
                    }
                    array = ms.ToArray();
                }
            }
        }
        result = Convert.ToBase64String(array);

        return result;
    }

    public string Decrypt(string text) {
        var result = string.Empty;

        var iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(text);

        using (var aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key,aes.IV);
            using (var ms = new MemoryStream(buffer)) {
                using (var cryptoStream = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read)) {
                    using (var streamReader = new StreamReader(cryptoStream)) { 
                        result = streamReader.ReadToEnd();
                    }
                }
            }
        }

        return result;
    }
}
