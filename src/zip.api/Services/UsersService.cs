using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zip.api.Models;
using zip.api.Repositories;

namespace zip.api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public IEnumerable<User> GetUsers()
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

        public void CreateUserAccount(Guid userId, Account account)
        {
            throw new NotImplementedException();
        }
    }
}
