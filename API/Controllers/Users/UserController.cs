using Application.Authentication.Query;
using Application.Users.Commands;
using Application.Users.DTO;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest user)
        {
            var Query = new CreateUserCommand(user);
            await _mediator.Send(Query);
            return Ok("User Created Successfully");
        }

        [Authorize]
        [HttpGet("GetLoggedInUserDetails")]
        public async Task<LoggedInUserProfile> GetCurrentUser()
        {
            var query = new GetLoggedInUserProfileQuery();
            return await _mediator.Send(query);
        }
    }
}
