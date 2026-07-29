using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.DTOs.User;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserDto> GetCurrentUserAsync();
        Task<UserDto> GetUserDetailsByUserIdAsync(int userId);
        Task<IEnumerable<ConversationDto>> GetPeopleToChat();
    }
}
