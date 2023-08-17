namespace SharedLibrary.HttpClientProviders;

public interface IEncriptionProvider
{
    string Encrypt(string text);

    string Decrypt(string text);
}
