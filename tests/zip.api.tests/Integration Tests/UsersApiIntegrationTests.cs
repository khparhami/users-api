using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Xunit;
using zip.api.Requests;

namespace zip.api.tests.Integration_Tests
{
    public class UsersApiIntegrationTests
    {
        private readonly UsersClient _client;

        public UsersApiIntegrationTests()
        {
            _client = new UsersClient(new HttpClient {BaseAddress = new Uri("http://localhost:5000")});
        }

        [Fact]
        public void GetAllUsersShouldReturnAListOfUsersWithStatusCodeOf200()
        {
            var result = _client.GetUsers();
            result.StatusCode.Should().BeEquivalentTo(200);
        }

        [Fact]
        public void GetAllUsersByIdShouldReturnAUsersWithStatusCodeOf200()
        {
            var userRequest = new CreateUserRequest
            {
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 20000,
                Name = "testing"
            };
            var result = _client.CreateUser(userRequest);

            result.StatusCode.Should().BeEquivalentTo(200);
        }
    }
}