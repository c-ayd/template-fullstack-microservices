namespace Shared.Crypto.Exceptions
{
    public class InvalidEncryptionKeySizeException : Exception
    {
        public InvalidEncryptionKeySizeException(int expectedSize, int keySize)
            : base($"The size of the key is expected to be {expectedSize} bytes long. The given key's size is {keySize} bytes.")
        {
        }
    }
}
