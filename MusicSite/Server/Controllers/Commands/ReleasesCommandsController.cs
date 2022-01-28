using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Commands;
using MusicSite.Server.Commands.Releases;
using MusicSite.Shared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers.Commands
{
    [ApiController, Route(Routing.ReleasesCrudController), Authorize]
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
            [FromBody] ReleaseSharedEditMode release,
            CancellationToken cancel
        )
        {
            _logger.LogDebug("CreateRelease command. Input data: {Release}", release);
            var command = new CreateReleaseCommand(release);
            try
            {
                var result_raw = await _mediator.Send(command, cancel);
                if (result_raw.ValidationFailed())
                {
                    return BadRequest(result_raw.ValidationFailuresStringArray());
                }
                var result = ValidatedResponse<int>.TryCastResponse(result_raw).Result;
                _logger.LogInformation("Created release ({Release}) with id={Result}", release, result);
                return Ok(result);
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating release ({Release}), details: {Message}", release, ex.Message);
                return BadRequest(ex.Message);
            } 
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRelease(
            int id,
            [FromBody] ReleaseSharedEditMode release,
            CancellationToken cancellationToken
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
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating release (id={Id}, data={Release}), details: {Message}", id, release, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRelease(
            int id,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("DeleteRelease(id={Id}) command", id);
            var command = new DeleteReleaseCommand(id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Deleted release (id={Id})", id);
                return Ok();
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting release (id={Id}), details: {Message}", id, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}/song")]
        public async Task<IActionResult> UpdateReleaseSong(
            int id,
            [FromBody] ReleaseSongShared releaseSong,
            CancellationToken cancellationToken
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
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating release song (ReleaseId={Id}, data={Song}), details: {Message}", id, releaseSong, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
