using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;
        
        public ChatRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
        }
    }
}
