using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zip.api.Models;
using zip.api.Requests;
using zip.api.Services;

namespace zip.api.Controllers
{
    [Route("api/" + Constants.ApiVersion.V1 + "/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Accepted();
        }

        [HttpPost(Name = "CreateUser")]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {
            return Accepted();
        }

        [HttpGet("{userId:guid}", Name = "GetPartnerUser")]
        public ActionResult<User> GetUsers(Guid userId)
        {
            return Accepted();
        }

        [HttpGet("{userId:guid}/Accounts", Name = "GetUserAccount")]
        public ActionResult<IEnumerable<Account>> GetUserAccount(Guid userId)
        {
            return Accepted();
        }

        [HttpPost("{userId:guid}/Accounts", Name = "CreateUserAccount")]
        public ActionResult CreateUserAccount(Guid userId, [FromBody] CreateUserAccountRequest userAccount)
        {
            return Accepted();
        }
    }
}
