using System;
using System.Collections.Generic;
using zip.api.Models;

namespace zip.api.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid userId);

        void CreateUser(User user);

        void AddAccount(Guid userId, Account account);
    }
}
