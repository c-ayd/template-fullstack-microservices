using System.Security.Cryptography;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Infrastructure.Authentication
{
    public class JwtKeyService : IJwtKeyService
    {
        public RsaSecurityKey PrivateKey { get; private set; }
        public RsaSecurityKey PublicKey { get; private set; }
        
        public JwtKeyService(IOptions<JwtSettings> jwtSettings)
        {
            var privatePem = File.ReadAllText(jwtSettings.Value.PrivateKeyPath);
            PrivateKey = LoadKey(jwtSettings.Value.KeyId, privatePem, isPrivate: true);

            var publicPem = File.ReadAllText(jwtSettings.Value.PublicKeyPath);
            PublicKey = LoadKey(jwtSettings.Value.KeyId, publicPem, isPrivate: false);
        }

        private RsaSecurityKey LoadKey(string keyId, string pem, bool isPrivate)
        {
            var rsa = RSA.Create();
            rsa.ImportFromPem(pem);

            return new RsaSecurityKey(rsa)
            {
                KeyId = keyId
            };
        }
    }
}