using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface ITokenGenerateService
    {
        string GenerateJwtToken(User newUser);
    }
}
