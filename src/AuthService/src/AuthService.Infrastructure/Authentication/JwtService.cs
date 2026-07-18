using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Application.Abstractions.Authentication;
using AuthService.Application.Dtos.Authentication;
using AuthService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Crypto;

namespace AuthService.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtKeyService _jwtKeyService;

        public JwtService(
            IOptions<JwtSettings> jwtSettings,
            IJwtKeyService jwtKeyService)
        {
            _jwtSettings = jwtSettings.Value;
            _jwtKeyService = jwtKeyService;
        }

        public JwtDto GenerateTokens(ICollection<Claim>? claims = null, DateTime? notBefore = null)
        {
            var now = DateTime.UtcNow;
            var accessTokenExpirationDate = now.AddMinutes(_jwtSettings.AccessTokenLifespanInMinutes);
            var refreshTokenExpirationDate = now.AddDays(_jwtSettings.RefreshTokenLifespanInDays);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims ?? Enumerable.Empty<Claim>(),
                notBefore: notBefore,
                expires: accessTokenExpirationDate,
                signingCredentials: new SigningCredentials(_jwtKeyService.PrivateKey, SecurityAlgorithms.RsaSha256)
            );

            return new JwtDto(
                AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken: TokenGenerator.GenerateBase64UrlSafe(),
                AccessTokenExpirationDate: accessTokenExpirationDate,
                RefreshTokenExpirationDate: refreshTokenExpirationDate
            );
        }
    }
}