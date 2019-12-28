using System;
using System.Collections.Generic;
using zip.api.Entities;

namespace zip.api.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid userId);

        User CreateUser(User user);

        bool UpdateUser(User user);

        User GetUserByEmail(string email);

        bool DeleteUser(Guid userId);
    };
}
