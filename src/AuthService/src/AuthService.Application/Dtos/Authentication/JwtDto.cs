namespace AuthService.Application.Dtos.Authentication
{
    public record JwtDto(
        string AccessToken,
        DateTime AccessTokenExpirationDate,
        string RefreshToken,
        DateTime RefreshTokenExpirationDate
    );
}
