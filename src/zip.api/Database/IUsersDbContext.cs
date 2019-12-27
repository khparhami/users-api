using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using zip.api.Entities;

namespace zip.api.Database
{
    public interface IUsersDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}
