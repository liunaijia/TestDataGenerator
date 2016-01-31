namespace DataProducer.Data
{
    public class DatabaseFactory
    {
        public static IDatabase CreateDatabase(string connectionStringName = "*")
        {
            return new ConnectionDatabase(connectionStringName);
        }
    }
}