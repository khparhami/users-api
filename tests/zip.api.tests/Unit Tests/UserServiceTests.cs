using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;
using zip.api.Entities;
using zip.api.Exceptions;
using zip.api.Repositories;
using zip.api.Services;

namespace zip.api.tests
{
    public class UserServiceTests
    {
        private readonly IUsersRepository _mockedUserRepository;

        public UserServiceTests()
        {
            _mockedUserRepository = Substitute.For<IUsersRepository>();
        }

        [Fact]
        public void CreateUserShouldThrowAnExceptionIfUserWithSameEmailExists()
        {
            var existingUser = new User()
            {
                UserId = Guid.NewGuid(),
                Email = "test@test.com",
                MonthlyExpenses = (decimal)1000.01,
                MonthlySalary = 2000
            };

            var creatingUser = new User()
            {
                UserId = Guid.NewGuid(),
                Email = "test@test.com",
                MonthlyExpenses = 10000,
                MonthlySalary = 20000
            };
            _mockedUserRepository.GetUserByEmail(existingUser.Email).Returns(existingUser);
            var target = new UsersService(_mockedUserRepository);

            target.Invoking(service => service.CreateUser(creatingUser))
                .Should().Throw<UserAlreadyExistsException>()
                .WithMessage("User already exist with provided email address");

        }

        [Fact]
        public void CreateUSerAccountShouldThrowAnExceptionIfNetIncomeIsLessThan1000()
        {

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = "test@test.com",
                MonthlyExpenses = (decimal)1000.01,
                MonthlySalary = 2000
            };

            _mockedUserRepository.GetUserById(user.UserId).Returns(user);
            var target = new UsersService(_mockedUserRepository);

            target.Invoking(service => service.CreateUserAccount(user.UserId, new Account()))
                .Should().Throw<InsufficientCreditException>()
                .WithMessage("User does not have sufficient credit");

        }

        [Fact]
        public void GetAllUsersShouldReturnAListOfUsersWithCorrectSchema()
        {
            var users = new List<User>
            {
                new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = "test@test.com",
                    MonthlyExpenses = 1000,
                    MonthlySalary = 2000,
                    Accounts = new List<Account>
                    {
                        new Account
                        {
                            AccountId = Guid.NewGuid(),
                            Balance = 1000,
                            Currency = "AUD"
                        }
                    }
                },
                new User()
                {
                    UserId = Guid.NewGuid(),
                    Email = "test2@test.com",
                    MonthlyExpenses = 10000,
                    MonthlySalary = 20000,
                    Accounts = new List<Account>()
                }
            };
            _mockedUserRepository.GetAllUsers().Returns(users);

            var target = new UsersService(_mockedUserRepository);

            var result = target.GetUsers();

            result.Should().BeEquivalentTo(users);
        }

        [Fact]
        public void GetAUserByIdShouldReturnAUserWithCorrectSchema()
        {
            var expectedUser = new User
            {
                UserId = Guid.NewGuid(),
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 2000,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountId = Guid.NewGuid(),
                        Balance = 1000,
                        Currency = "AUD"
                    }
                }
            };

            _mockedUserRepository.GetUserById(expectedUser.UserId).Returns(expectedUser);

            var target = new UsersService(_mockedUserRepository);

            var result = target.GetUserById(expectedUser.UserId);

            result.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void GetAUserByIdShouldReturnNullIfUserIsNotFound()
        {
            var notExistingId = Guid.NewGuid();

            _mockedUserRepository.GetUserById(notExistingId).ReturnsNull();

            var target = new UsersService(_mockedUserRepository);

            var result = target.GetUserById(notExistingId);

            result.Should().BeNull();
        }
    }
}
