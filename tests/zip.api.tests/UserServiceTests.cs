using System;
using NSubstitute;
using Xunit;
using zip.api.Exceptions;
using zip.api.Models;
using zip.api.Repositories;
using zip.api.Services;

namespace zip.api.tests
{
    public class UserServiceTests
    {

        [Fact]
        public void CreateUserShouldThrowAnExceptionIfUSerWithSameEmailExists()
        {



        }

        [Fact]
        public void CreateUSerAccountShouldThrowAnExceptionIfNetIncomeIsLessThan1000()
        {
            //Arrange
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = "test@test.com",
                MonthlyExpenses = (decimal) 1000.01,
                MonthlySalary = 2000,

            };
            var mockedUserRepository = Substitute.For<IUsersRepository>();
            mockedUserRepository.GetUserById(user.UserId).Returns(user);
            var target = new UsersService(mockedUserRepository);


            //Act
            Assert.Throws<InsufficientCreditException>(() => target.CreateUserAccount(new Guid(), new Account()));

        }

        [Fact]
        public void GetAllUsersShouldReturnAListOfUsersWithCorrectSchema()
        {

        }

        [Fact]
        public void GetAUserByIdShouldReturnAUserWithCorrectSchema()
        {

        }

        [Fact]
        public void GetAUserByIdShouldThrowAnExceptionIfUserIsNotFound()
        {

        }

    }
}
