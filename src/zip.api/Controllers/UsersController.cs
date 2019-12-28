using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using Swashbuckle.AspNetCore.Swagger;
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

        /// <summary>
        /// Returns a List of users
        /// </summary>
        /// <returns>A list of users</returns>
        /// <response code="200">Returns a json array of users</response>
        /// <response code="500">An error has occurred</response>
        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<User>> GetGetUsers()
        {
            try
            {
                var result = _usersService.GetUsers();
                return Ok(result.Model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <param name="user">User request object</param>
        /// <returns>Created user object</returns>
        /// <response code="201">Returns the newly created user object</response>
        /// <response code="422">Unprocessable Entity, email address already exists.</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(User), 201)]
        public ActionResult<User> CreateUser([FromBody] CreateUserRequest user)
        {
            try
            {
                var result = _usersService.CreateUser(_mapper.Map<User>(user));
                return StatusCode((int)result.StatusCode, result.Model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Returns a user by userId
        /// </summary>
        /// <returns>Returns a user object</returns>
        /// <param name="userId">Id of the user</param>
        /// <response code="200">Returns a user object by userId</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An error has occurred</response>
        [HttpGet("{userId:guid}", Name = "GetUser")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(User), 200)]
        public ActionResult<User> GetUsers(Guid userId)
        {
            try
            {
                var result = _usersService.GetUserById(userId);
                return StatusCode((int)result.StatusCode, result.Model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Returns a list of user accounts
        /// </summary>
        /// <returns>Returns a user account objects</returns>
        /// <param name="userId">Id of the user</param>
        /// <response code="200">Returns an account object by userId</response>
        /// <response code="404">User not found</response>
        /// <response code="500">An error has occurred</response>
        [HttpGet("{userId:guid}/Accounts", Name = "GetUserAccount")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(User), 200)]
        public ActionResult<IEnumerable<Account>> GetUserAccount(Guid userId)
        {
            try
            {
                var result = _usersService.GetUserById(userId);
                return StatusCode((int) result.StatusCode, result.Model.Accounts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Adds an account to a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="userAccount">Account to be created under the user</param>
        /// <response code="201">Account created for the user</response>
        /// <response code="404">User not found</response>
        /// <response code="422">Unprocessable Entity, Insufficient credit</response>
        /// <response code="400">A validation error has occurred or there was something wrong with the request.</response>
        /// <response code="500">An error has occurred</response>
        [HttpPost("{userId:guid}/Accounts", Name = "CreateUserAccount")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public ActionResult CreateUserAccount(Guid userId, [FromBody] CreateUserAccountRequest userAccount)
        {
            try
            {
                var result = _usersService.CreateUserAccount(userId, _mapper.Map<Account>(userAccount));
                return StatusCode((int) result.StatusCode, result.Model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{userId:guid}")]
        public ActionResult<bool> Delete(Guid userId)
        {
            try
            {
                var result = _usersService.DeleteUser(userId);
                return StatusCode((int) result.StatusCode, result.Model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e);
            }
            
        }
    }
}
