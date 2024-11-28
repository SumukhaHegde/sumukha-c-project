using Application.Authentication.Common;
using Application.Authentication.DTO;
using Application.Authentication.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Login
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {

            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var loginQuery = new LoginQuery(request);
            var authResult = await _mediator.Send(loginQuery);
            if (authResult.IsSuccess)
            {
                var response = _mapper.Map<AuthenticationResult>(authResult.Value);
                return Ok(response);
            }
            else
            {
                return BadRequest(authResult.Errors[0].Message);
            }

        }

    }
}
