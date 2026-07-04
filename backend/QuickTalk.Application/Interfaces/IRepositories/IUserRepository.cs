using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserIdAsync(int userId);
        Task<IEnumerable<User>> GetPeopleToChat(int userId);
    }
}
