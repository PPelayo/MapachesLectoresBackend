using MongoDB.Driver;

namespace MapachesLectoresBackend.Core.Data.Db
{
    public class MongoDbDatabase
    {

        public MongoClient MongoClient { get; }
        public IMongoDatabase MongoDatabase { get; }

        public MongoDbDatabase(IConfiguration configuration)
        {
            MongoClient = new MongoClient(configuration.GetValue<string>("mongoDbConnectionString"));
            MongoDatabase = MongoClient.GetDatabase(configuration.GetValue<string>("mongoDbName"));
        }

    }
}
