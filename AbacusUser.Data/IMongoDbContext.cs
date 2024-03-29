﻿namespace AbacusUser.Data;

public interface IMongoDbContext
{
    IMongoDatabase Database { get; }
    IMongoCollection<T> GetCollection<T>(string collectionName) where T : DbEntity => Database.GetCollection<T>(collectionName);
    IMongoCollection<T> GetCollection<T>() where T : DbEntity => Database.GetCollection<T>(typeof(T).Name);
}
