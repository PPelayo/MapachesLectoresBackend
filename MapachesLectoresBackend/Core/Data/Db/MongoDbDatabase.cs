using MongoDB.Driver;

namespace MapachesLectoresBackend.Core.Data.Db
{
    public class MongoDbDatabase
    {

        public MongoClient MongoClient { get; }
        public IMongoDatabase MongoDatabase { get; }

        public MongoDbDatabase(IConfiguration configuration)
        {
            var connString = configuration.GetValue<string>("MONGODB_CONNECTION_STRING");
            var dbName = configuration.GetValue<string>("MONGODB_DBNAME");
            MongoClient = new MongoClient(connString);
            MongoDatabase = MongoClient.GetDatabase(dbName);
        }

    }
}
