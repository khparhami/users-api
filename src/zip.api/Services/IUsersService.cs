using System;
using System.Collections.Generic;
using zip.api.Entities;

namespace zip.api.Services
{
    public interface IUsersService
    {
        ServiceResult<IEnumerable<User>> GetUsers();

        ServiceResult<User> GetUserById(Guid userId);

        ServiceResult<User> CreateUser(User user);

        ServiceResult<bool> CreateUserAccount(Guid userId, Account account);

        ServiceResult<bool> DeleteUser(Guid userId);
    }
}
