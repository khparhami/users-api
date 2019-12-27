using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using zip.api.Models;

namespace zip.api.Database
{
    public class IUsersDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}
