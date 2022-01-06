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
        public async Task<IActionResult> CreateRelease(
            CancellationToken cancel,
            [FromBody] ReleaseSharedEditMode release
        )
        {
            _logger.LogInformation("CreateRelease command. Input data: {Release}", release);
            var command = new CreateReleaseCommand(release);
            try
            {
                var result = await _mediator.Send(command, cancel);
                _logger.LogInformation("Created release ({Release}) with id={Result}", release, result);
                return Ok(result);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating release ({Release}), details: {Message}", release, ex.Message);
                return BadRequest(ex.Message);
            } 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRelease(
            CancellationToken cancellationToken,
            int id,
            [FromBody] ReleaseSharedEditMode release
        )
        {
            _logger.LogInformation("UpdateRelease(id={Id}) command. Input data: {Release}", id, release);
            var command = new UpdateReleaseCommand(release, id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Updated release (id={Id}, data={Release})", id, release);
                return Ok();
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating release (id={Id}, data={Release}), details: {Message}", id, release, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelease(
            CancellationToken cancellationToken,
            int id
        )
        {
            _logger.LogInformation("DeleteRelease(id={Id}) command.", id);
            var command = new DeleteReleaseCommand(id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Deleted release (id={Id})", id);
                return Ok();
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting release (id={Id}), details: {Message}", id, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/song")]
        public async Task<IActionResult> UpdateReleaseSong(
            CancellationToken cancellationToken,
            int id,
            [FromBody] ReleaseSongShared releaseSong
        )
        {
            _logger.LogInformation("UpdateReleaseSong(ReleaseId={Id}) command. Input data: {Song}", id, releaseSong);
            var command = new UpdateReleaseSongCommand(releaseSong, id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Updated release (id={ReleaseId}, data={Song})", id, releaseSong);
                return Ok();
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating release song (ReleaseId={Id}, data={Song}), details: {Message}", id, releaseSong, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
