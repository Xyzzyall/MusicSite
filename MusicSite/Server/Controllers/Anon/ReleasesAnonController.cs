using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.Anon.Release;
using MusicSite.Shared;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Controllers.Anon
{
    [ApiController, Route(Routing.AnonReleasesController)]
    public class ReleasesAnonController : Controller
    {
        private readonly IMediator _mediator;

        public ReleasesAnonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<ReleaseSharedIndex[]>> Index( 
            [FromQuery] string language,
            CancellationToken cancel,
            [FromQuery] int page = 0,
            [FromQuery] int recordsPerPage = 100
        )
        {
            var query = new IndexReleasesQuery(language, page, recordsPerPage);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }
        
        [HttpGet("{codename}")]
        public async Task<ActionResult<ReleaseSharedIndex>> GetRelease(
            string codename,
            [FromQuery] string language,
            CancellationToken cancel
        )
        {
            var query = new ReleaseDetailQuery(codename, language); 
            var result = await _mediator.Send(query, cancel);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("exists/{codename}")]
        public async Task<ActionResult<bool>> ReleaseExits(
            string codename,
            [FromQuery] string language,
            CancellationToken cancel
        )
        {
            var query = new ReleaseExitstsQuery(codename, language);
            var exists = await _mediator.Send(query, cancel);
            return Ok(exists);
        }
    }
}
