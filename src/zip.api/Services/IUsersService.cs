using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zip.api.Models;

namespace zip.api.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetUsers();

        User GetUserById(Guid userId);

        void CreateUser();

        void CreateUserAccount(Guid userId);
    }
}
