using Anamnese.API.ORM.Entity;

namespace Anamnese.API.Application.Services.Token
{
    public interface ITokenService
    {        
         string GenerateToken(string key, string issuer, string audience, ProfissionalModel profissional);
         string? GetUserEmail();
         int GetUserId();
        
    }
}
