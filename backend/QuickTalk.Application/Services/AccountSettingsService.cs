using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Application.Services
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly IAccountSettingsRepository _accountSettingsRepository;
        public AccountSettingsService(IAccountSettingsRepository accountSettingsRepository)
        {
            _accountSettingsRepository = accountSettingsRepository;
        }
    }
}
