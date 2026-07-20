using System.Security.Cryptography;

namespace Shared.Crypto.Options
{
    public record HashOptions(
        Func<HashAlgorithm> Algorithm,
        int SaltSize
    );
}