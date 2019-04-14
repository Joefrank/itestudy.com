
namespace CMCommon.Security.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string str);

        string Encrypt(string clearText, string Password);

        byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV);

        string Decrypt(string str);

        string Decrypt(string cipherText, string Password);

        byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV);
    }
}
