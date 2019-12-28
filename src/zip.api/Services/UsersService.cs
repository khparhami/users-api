using System;
using System.Collections.Generic;
using System.Net;
using zip.api.Entities;
using zip.api.Repositories;

namespace zip.api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        private const decimal RequiredCredit = (decimal) 1000.00;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public ServiceResult<IEnumerable<User>> GetUsers()
        {
            var users = _usersRepository.GetAllUsers();

            return new ServiceResult<IEnumerable<User>>(users, HttpStatusCode.OK);
        }

        public ServiceResult<User> GetUserById(Guid userId)
        {
           var user = _usersRepository.GetUserById(userId);

           var statusCode = HttpStatusCode.OK;

           if (user == null)
           {
               statusCode = HttpStatusCode.NotFound;
           }

           return new ServiceResult<User>(user, statusCode);
        }

        public ServiceResult<User> CreateUser(User user)
        {
            var existingUser = _usersRepository.GetUserByEmail(user.Email);

            if (existingUser != null)
            {
                return new ServiceResult<User>(null, HttpStatusCode.UnprocessableEntity);
            }

            var  createdUser = _usersRepository.CreateUser(user);

            return new ServiceResult<User>(createdUser, HttpStatusCode.Created);
        }

        public ServiceResult<bool> CreateUserAccount(Guid userId, Account account)
        {
            var user = this._usersRepository.GetUserById(userId);

            if (user == null)
            {
                return new ServiceResult<bool>(false, HttpStatusCode.NotFound);
            }


            if (user.MonthlySalary - user.MonthlyExpenses < RequiredCredit)
            {
                return new ServiceResult<bool>(false, HttpStatusCode.UnprocessableEntity);
            }

            account.AccountId = Guid.NewGuid();
            user.Accounts.Add(account);
            var result = _usersRepository.UpdateUser(user);

            return new ServiceResult<bool>(result, HttpStatusCode.Created);
        }

        public ServiceResult<bool> DeleteUser(Guid userId)
        {
            var result = _usersRepository.DeleteUser(userId);
            return new ServiceResult<bool>(result, HttpStatusCode.OK);
        }


    }
}
