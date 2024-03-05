using CatalogAPI.Models;

namespace CatalogAPI.Services.Token
{
    public interface ITokenService
    {
        string GenerateToken(string key, string issuer, string audience, DoctorModel user);
        string? GetUserEmail();
        int GetUserId();
    }
}
