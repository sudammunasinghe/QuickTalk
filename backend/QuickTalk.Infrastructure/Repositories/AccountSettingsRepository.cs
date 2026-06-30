using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;

namespace QuickTalk.Infrastructure.Repositories
{
    public class AccountSettingsRepository : IAccountSettingsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;
        public AccountSettingsRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
        }
    }
}
