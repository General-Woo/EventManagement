using MongoDB.Driver;

namespace EventManagementApp.Server.Core.Interfaces
{
    public interface IMongoCollectionFactory
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
