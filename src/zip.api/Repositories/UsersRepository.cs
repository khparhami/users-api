using System;
using System.Collections.Generic;
using zip.api.Database;
using zip.api.Models;

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
            throw new NotImplementedException();
        }

        public User GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void AddAccount(Guid userId, Account account)
        {
            throw new NotImplementedException();
        }
    }
}
