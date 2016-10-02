using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices
{
    public class MongoFileClient
    {
        public IMongoDatabase _db { get; set; }
        public MongoFileClient(string database)
        {
            _db = GetDatabase(database);
        }
        public IMongoDatabase GetDatabase(string dbName)
        {

            const string connectionString = "mongodb://localhost:27017";

            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            return client.GetDatabase(dbName);
        }

        public async Task InsertOne<T>(T entity)
        {
            var collection = _db.GetCollection<T>(entity.GetType().Name);
            await collection.InsertOneAsync(entity);
        }

        public async Task<List<T>> GetCollectionById<T>(string Id)
        {
            var filter = Builders<T>.Filter.Eq("_id", Id);

            //get mongodb collection
            var t = typeof(T);
            var collection = _db.GetCollection<T>(t.GetType().Name);

            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetCollectionByFilter<T>(FilterDefinition<T> filter)
        {
            var t = typeof(T);
            var collection = _db.GetCollection<T>(t.Name);
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<T> GetItemById<T>(T entity, string GuidId)
        {
            var filter = Builders<T>.Filter.Eq("GuidId", GuidId);

            //get mongodb collection
            var collection = _db.GetCollection<T>(entity.GetType().Name);

            var result = await collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<T>> GetCollection<T>(T entity)
        {
            //get mongodb collection
            var collection = await _db.GetCollection<T>(entity.GetType().Name).Find(_ => true).ToListAsync();

            return collection;
        }
    }
}
