using System;
using System.Net.Http;
using zip.api.Entities;
using zip.api.Requests;

namespace zip.api.integration
{
    public class UsersClient: ClientBase
    {
        private readonly HttpClient _client;
        private const string RequestBaseUri = "api/v1/users";

        public UsersClient(HttpClient client)
        {
            _client = client;
        }

        public HttpResponseMessage CreateUser(CreateUserRequest request)
        {
            return _client.PostAsync(RequestBaseUri, GetJsonStringContent(request)).Result;
        }

        public HttpResponseMessage GetUsers()
        {
            return _client.GetAsync(RequestBaseUri).Result;
        }

        public HttpResponseMessage GetUserById(Guid userId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_client.BaseAddress}{RequestBaseUri}/{userId}"),
            };

            return _client.SendAsync(httpRequestMessage).Result;
        }


        public HttpResponseMessage GetUserAccounts(Guid userId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_client.BaseAddress}{RequestBaseUri}/{userId}/accounts"),
            };

            return _client.SendAsync(httpRequestMessage).Result;
        }

        public HttpResponseMessage CreateAccount(Guid userId, CreateUserAccountRequest request)
        {
            return _client.PostAsync(new Uri($"{_client.BaseAddress}{RequestBaseUri}/{userId}/accounts"),
                GetJsonStringContent(request)).Result;
        }

        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_client.BaseAddress}{RequestBaseUri}/{userId}"),
            };

            return _client.SendAsync(httpRequestMessage).Result;
        }
    }
}
