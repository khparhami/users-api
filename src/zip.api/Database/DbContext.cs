using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using zip.api.Config;
using zip.api.Models;

namespace zip.api.Database
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _db;

        public DbContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");

    }
}
