namespace AbacusUser.Data
{
    public static class MongoDbExtensions
    {
        public static IMongoQueryable<T> Query<T>(this IMongoDbContext context)
            where T : DbEntity
            => context.GetCollection<T>().AsQueryable();
    }
}
