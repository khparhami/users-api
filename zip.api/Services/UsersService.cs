using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zip.api.Models;

namespace zip.api.Services
{
    public class UsersService : IUsersService
    {
        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser()
        {
            throw new NotImplementedException();
        }

        public void CreateUserAccount(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
