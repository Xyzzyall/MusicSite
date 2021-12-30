using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Commands.Releases;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers.Commands
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ReleasesCommandsController : Controller
    {
        private readonly ILogger<ReleasesCommandsController> _logger;
        private readonly IMediator _mediator;

        public ReleasesCommandsController(ILogger<ReleasesCommandsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> CreateRelease(
            CancellationToken cancel,
            [FromBody] ReleaseSharedEditMode release
        )
        {
            var command = new CreateReleaseCommand(release);
            var result = await _mediator.Send(command, cancel);
            return View(result); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRelease()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelease()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}/song")]
        public async Task<IActionResult> UpdateReleaseSong()
        {
            throw new NotImplementedException();
        }
    }
}
