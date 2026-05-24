using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealityHub.Application.UseCases.Rounds.GetCurrentRoundResults;

namespace RealityHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoundController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current/results")]
        public async Task<IActionResult> GetCurrentResults()
        {
            var result = await _mediator.Send(new GetCurrentRoundResultsQuery());

            return Ok(result);
        }
    }
}
