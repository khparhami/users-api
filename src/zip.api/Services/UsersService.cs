using System;
using System.Collections.Generic;
using zip.api.Entities;
using zip.api.Exceptions;
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
        public IEnumerable<User> GetUsers()
        {
            return _usersRepository.GetAllUsers();
        }

        public User GetUserById(Guid userId)
        {
            return _usersRepository.GetUserById(userId);
        }

        public void CreateUser(User user)
        {
            var existingUser = _usersRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            _usersRepository.CreateUser(user);
        }

        public void CreateUserAccount(Guid userId, Account account)
        {
            var user = this._usersRepository.GetUserById(userId);
            if (user.MonthlySalary - user.MonthlyExpenses < RequiredCredit)
            {
                throw new InsufficientCreditException();
            }

            account.AccountId = Guid.NewGuid();
            user.Accounts.Add(account);
            _usersRepository.UpdateUser(user);
        }
    }
}
