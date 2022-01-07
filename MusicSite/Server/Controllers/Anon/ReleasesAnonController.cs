using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Models;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Server.Queries.Anon.Release;
using MediatR;

namespace MusicSite.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ReleasesAnonController : Controller
    {
        private readonly MusicSiteServerContext _context;
        private readonly ILogger<ReleasesAnonController> _logger;
        private readonly IMediator _mediator;

        public ReleasesAnonController(MusicSiteServerContext context, ILogger<ReleasesAnonController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<ReleaseSharedIndex[]>> Index( 
            [FromQuery] string language,
            CancellationToken cancel,
            [FromQuery] int page = 0,
            [FromQuery] int records_per_page = 100
        )
        {
            var query = new IndexReleasesQuery(language, page, records_per_page);
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
