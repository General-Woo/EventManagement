using EventManagementApp.Server.Core.Interfaces;
using MongoDB.Driver;

namespace EventManagementApp.Server.Infrastucture
{
    public class MongoCollectionFactory : IMongoCollectionFactory
    {
        private readonly MongoClient _mongoClient;

        public MongoCollectionFactory(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var database = _mongoClient.GetDatabase("event-management-app");
            return database.GetCollection<T>(collectionName);
        }
    }
}
