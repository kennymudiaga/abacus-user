namespace AbacusUser.Data.Handlers;

public abstract class MongoDbHandler
{
    private readonly IMongoDbContext dbContext;
    protected MongoDbHandler(IMongoDbContext dbContext)
    {
        this.dbContext  = dbContext;
    }

    protected IMongoDatabase Database => dbContext.Database;

    protected IMongoCollection<T> GetCollection<T>() where T : DbEntity => dbContext.GetCollection<T>();

    protected IMongoCollection<T> GetCollection<T>(string collectionName) where T : DbEntity => Database.GetCollection<T>(collectionName);

    protected IMongoQueryable<T> Query<T>() where T : DbEntity =>  GetCollection<T>().AsQueryable();

    protected IMongoQueryable<T> Query<T>(string collectionName) where T : DbEntity => GetCollection<T>(collectionName).AsQueryable();
}

public abstract class MongoDbHandler<T> : MongoDbHandler
    where T : DbEntity
{
    private readonly Lazy<IMongoQueryable<T>> _querySet;

    protected IMongoCollection<T> Collection { get; private set; }
    
    protected IMongoQueryable<T> Query => _querySet.Value;

    protected MongoDbHandler(IMongoDbContext dbContext) : base(dbContext)
    {
        Collection = GetCollection<T>();
        _querySet = new Lazy<IMongoQueryable<T>>(Collection.AsQueryable());
    }
}
