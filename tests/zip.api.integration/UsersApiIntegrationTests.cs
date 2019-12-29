using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using zip.api.Entities;
using zip.api.Requests;

namespace zip.api.integration
{
    public class UsersApiIntegrationTests
    {
        private readonly UsersClient _client;

        public UsersApiIntegrationTests()
        {
            _client = new UsersClient(new HttpClient {BaseAddress = new Uri("http://localhost:5000")});
            var usersResponse = _client.GetUsers();
            var allUsers = DeserializeHttpResponseMessageContentAsync<IEnumerable<User>>(usersResponse).Result;
            DeleteUsers(allUsers.ToList());
        }

        [Fact]
        public void GetAllUsersShouldReturnAListOfUsersWithStatusCodeOf200()
        {
            var usersResponse = _client.GetUsers();
            var allUsers = DeserializeHttpResponseMessageContentAsync<IEnumerable<User>>(usersResponse).Result;
            usersResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            allUsers.Should().BeEmpty();
        }

        [Fact]
        public void GetAllUsersByIdShouldReturnAUsersWithStatusCodeOf200()
        {
            var usersToBeDeleted = new List<User>();

            var userRequest = new CreateUserRequest
            {
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 20000,
                Name = "testing"
            };

            var httpResponseMessage = _client.CreateUser(userRequest);
            var createdUser = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            usersToBeDeleted.Add(createdUser);

            httpResponseMessage = _client.GetUserById(createdUser.UserId);
            var testingUser = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            testingUser.Should().BeEquivalentTo(userRequest);

            httpResponseMessage.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            DeleteUsers(usersToBeDeleted);
        }

        [Fact]
        public void GetAllUsersByIdShouldReturn404ForANoneExistingUserId()
        {
            var httpResponseMessage = _client.GetUserById(Guid.NewGuid());
            var result = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            result.Should().BeNull();
            httpResponseMessage.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);

        }


        [Fact]
        public void CreateUserShouldReturnCreatedUserObjectWithStatusCodeOf201()
        {
            var usersToBeDeleted = new List<User>();

            var userRequest = new CreateUserRequest
            {
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 20000,
                Name = "testing"
            };

            var httpResponseMessage = _client.CreateUser(userRequest);
            var createdUser = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            usersToBeDeleted.Add(createdUser);

            httpResponseMessage.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);

            createdUser.Should().BeEquivalentTo(userRequest);

            DeleteUsers(usersToBeDeleted);

        }


        [Fact]
        public void CreateUserShouldReturn422StatusCodeForADuplicateEmailRequest()
        {
            var usersToBeDeleted = new List<User>();

            var userRequest = new CreateUserRequest
            {
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 20000,
                Name = "testing"
            };

            var httpResponseMessage = _client.CreateUser(userRequest);
            var createdUser = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            usersToBeDeleted.Add(createdUser);
            

            httpResponseMessage = _client.CreateUser(userRequest);
            var result = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            httpResponseMessage.StatusCode.Should().BeEquivalentTo(HttpStatusCode.UnprocessableEntity);

            result.Should().BeNull();

            DeleteUsers(usersToBeDeleted);

        }

        [Fact]
        public void CreateUserAccountShouldReturnStatusCodeOf201AndItShouldBeAddedToUserObject()
        {
            var usersToBeDeleted = new List<User>();

            var userRequest = new CreateUserRequest
            {
                Email = "test@test.com",
                MonthlyExpenses = 1000,
                MonthlySalary = 20000,
                Name = "testing"
            };

            var httpResponseMessage = _client.CreateUser(userRequest);
            var createdUser = DeserializeHttpResponseMessageContentAsync<User>(httpResponseMessage).Result;

            usersToBeDeleted.Add(createdUser);

            httpResponseMessage = _client.CreateAccount(createdUser.UserId, new CreateUserAccountRequest
            {
                Balance = 0,
                Currency = "AUD"
            });

            httpResponseMessage.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);

            httpResponseMessage = _client.GetUserAccounts(createdUser.UserId);
            var accounts = DeserializeHttpResponseMessageContentAsync<IEnumerable<Account>>(httpResponseMessage).Result.ToList();
            accounts.Count.Should().Be(1);

            DeleteUsers(usersToBeDeleted);

        }
        private void DeleteUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _client.DeleteUser(user.UserId);
            }
        }

        private async Task<TResponse> DeserializeHttpResponseMessageContentAsync<TResponse>(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var typedResponse = JsonConvert.DeserializeObject<TResponse>(content);
            return typedResponse;
        }
    }
}