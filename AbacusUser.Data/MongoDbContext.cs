namespace AbacusUser.Data;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; private set; }

    public MongoDbContext(IMongoClient mongoClient, MongoDbConfig config)
    {
        Database = mongoClient.GetDatabase(config.DbName);
    }
}
