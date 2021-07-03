namespace albelli.encryption.utility
{
    public interface ICryptoUtility
    {
        string Decrypt(string encryptedText);
        string Encrypt(string encryptString);
    }
}