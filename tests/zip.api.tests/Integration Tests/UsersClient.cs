using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using zip.api.Requests;

namespace zip.api.tests.Integration_Tests
{
    public class UsersClient: ClientBase
    {
        private readonly HttpClient _client;
        private const string RequestBaseUri = "/api/v1/users";

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
    }
}
