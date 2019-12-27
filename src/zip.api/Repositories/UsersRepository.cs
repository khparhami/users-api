using System;
using System.Collections.Generic;
using MongoDB.Driver;
using zip.api.Database;
using zip.api.Entities;

namespace zip.api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IUsersDbContext _usersDbContext;

        public UsersRepository(IUsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _usersDbContext.Users.Find(_ => true).ToList();
        }

        public User GetUserById(Guid userId)
        {
            var filter = Builders<User>.Filter.Eq(user => user.UserId, userId);
            return _usersDbContext.Users.Find(filter).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            _usersDbContext.Users.InsertOne(user);
        }

        public bool UpdateUser(User user)
        {
            var updateResult =
                _usersDbContext
                    .Users
                    .ReplaceOne(
                        filter: usr => usr.UserId == user.UserId,
                        replacement: user);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public User GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Email, email);
            return _usersDbContext.Users.Find(filter).FirstOrDefault();
        }
    }
}
