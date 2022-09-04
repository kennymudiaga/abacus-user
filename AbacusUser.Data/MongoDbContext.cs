namespace AbacusUser.Data;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; private set; }

    public MongoDbContext(IMongoClient mongoClient, MongoDbConfig config)
    {
        Database = mongoClient.GetDatabase(config.DbName);
    }
}

public class MongoDbContext<T> : MongoDbContext, IMongoDbContext<T>
    where T : DbEntity
{
    public IMongoCollection<T> Collection { get; private set; }
    public MongoDbContext(IMongoClient mongoClient, MongoDbConfig config) : base(mongoClient, config)
    {
        Collection = Database.GetCollection<T>(typeof(T).Name);
    }
}
