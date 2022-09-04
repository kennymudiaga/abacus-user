namespace AbacusUser.Data
{
    public static class MongoDbExtensions
    {
        public static IMongoQueryable<T> Query<T>(this IMongoDbContext context)
            where T : DbEntity
            => context.GetCollection<T>().AsQueryable();

        public static IMongoQueryable<T> Query<T>(this IMongoDbContext<T> context)
            where T : DbEntity
            => context.Collection.AsQueryable();
    }
}
