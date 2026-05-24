namespace QuickTalk.Infrastructure.Persistence.Sql.Helpers
{
    public interface ISqlQueryLoader
    {
        string Load(string folder, string fileName);
    }
}
