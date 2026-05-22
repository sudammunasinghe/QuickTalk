using System.Data;

namespace QuickTalk.Infrastructure.Persistence
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
