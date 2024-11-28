using Application.Activity.Commands;
using Application.Activity.DTO;
using Application.Activity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Activities
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetActivities")]
        public async Task<List<ActivityDto>> GetActivities()
        {
            var Query = new GetActivitiesQuery();
            var ActivityList = await _mediator.Send(Query);
            return ActivityList;
        }

        [HttpGet("GetActivityByID")]
        public async Task<ActionResult<ActivityDto>> GetActivityById([FromQuery, Required] int id)
        {
            if (id < 0) return BadRequest(string.Format("{0} is invalid", id));
            var Query = new GetActivityByIdQuery { Id = id };
            var Activity = await _mediator.Send(Query);
            if (Activity == null) return NotFound();
            return Activity;
        }

        [HttpPost("CreateActivity")]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest activity)
        {
            var Query = new CreateActivityCommand(activity);
            await _mediator.Send(Query);
            return Ok("Activity Created Successfully");
        }

        [HttpDelete("DeleteActivityById")]
        public async Task<IActionResult> DeleteActivityById([FromQuery, Required] int id)
        {
            var executeAsync = new DeleteActivityCommand { Id = id };
            await _mediator.Send(executeAsync);
            return Ok("Activity Deleted Successfully");
        }

        [HttpPut("UpdateActivity")]
        public async Task<IActionResult> UpdateActivity([FromBody, Required] UpdateActivityRequest request)
        {
            var command = new UpdateActivityCommand(request);
            var response = await _mediator.Send(command);
            if (response == null) return NotFound("Activity not found");
            if (response == "Only the host can update this activity.") return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("CancelActivity")]
        public async Task<IActionResult> CancelActivity([FromQuery, Required] int ActivityId)
        {
            var command = new CancelActivityCommand(ActivityId);
            var response = await _mediator.Send(command);
            if (response == null) return NotFound("Activity not found");
            if (response == "Only the host can cancel this activity.") return BadRequest(response);
            return Ok(response);
        }

    }
}
