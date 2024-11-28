using Application.Photos.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PhotosController
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
