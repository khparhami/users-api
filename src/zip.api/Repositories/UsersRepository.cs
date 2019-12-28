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
            return GetUser(userId);
        }

        public User CreateUser(User user)
        {
            user.UserId = Guid.NewGuid();
            _usersDbContext.Users.InsertOne(user);
            return GetUser(user.UserId);
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

        public bool DeleteUser(Guid userId)
        {
            var filter = Builders<User>.Filter.Eq(usr => usr.UserId, userId);
            var deleteResult = _usersDbContext
                .Users
                .DeleteOne(filter);
            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        private User GetUser(Guid id)
        {
            var filter = Builders<User>.Filter.Eq(user => user.UserId, id);
            return _usersDbContext.Users.Find(filter).FirstOrDefault();
        }
    }
}
