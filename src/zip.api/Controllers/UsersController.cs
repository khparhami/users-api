using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using zip.api.Entities;
using zip.api.Requests;
using zip.api.Services;

namespace zip.api.Controllers
{
    [Route("api/" + Constants.ApiVersion.V1 + "/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public UsersController(IMapper mapper,IUsersService usersService)
        {
            _mapper = mapper;
            _usersService = usersService;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_usersService.GetUsers());
        }

        [HttpPost(Name = "CreateUser")]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {
            _usersService.CreateUser(_mapper.Map<User>(user));
            return Ok();
        }

        [HttpGet("{userId:guid}", Name = "GetUser")]
        public ActionResult<User> GetUsers(Guid userId)
        {
            return Ok(_usersService.GetUserById(userId));
        }

        [HttpGet("{userId:guid}/Accounts", Name = "GetUserAccount")]
        public ActionResult<IEnumerable<Account>> GetUserAccount(Guid userId)
        {
            var user = _usersService.GetUserById(userId);
            return Ok(user.Accounts);
        }

        [HttpPost("{userId:guid}/Accounts", Name = "CreateUserAccount")]
        public ActionResult CreateUserAccount(Guid userId, [FromBody] CreateUserAccountRequest userAccount)
        {
            _usersService.CreateUserAccount(userId, _mapper.Map<Account>(userAccount));
            return Ok();
        }
    }
}
