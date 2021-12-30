using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Models;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ReleasesAnonController : Controller
    {
        private readonly MusicSiteServerContext _context;
        private readonly ILogger<ReleasesAnonController> _logger;

        public ReleasesAnonController(MusicSiteServerContext context, ILogger<ReleasesAnonController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("index/{author}")]
        public async Task<ActionResult<ReleaseSharedIndex[]>> IndexByAuthor(
            CancellationToken cancel, 
            string author,
            [FromQuery] string language,
            [FromQuery] int page = 0,
            [FromQuery] int records_per_page = 100
        )
        {
            var query = _context.Release
                .Where(release => release.Language == language && release.Author == author)
                .Skip(page * records_per_page)
                .Take(records_per_page);

            var query_result = await query.ToArrayAsync(cancel);
            ReleaseSharedIndex[] shared_releases = TransoformReleasesIndex(query_result);

            return View(shared_releases);
        }

        private static ReleaseSharedIndex[] TransoformReleasesIndex(Release[] query_result)
        {
            return query_result.Select(
                release => new ToReleaseSharedIndex(release)
            ).ToArray();
        }

        [HttpGet("{codename}")]
        public async Task<ActionResult<ReleaseSharedIndex>> GetRelease(
            CancellationToken cancel, 
            string codename,
            [FromQuery] string language
        )
        {
            var query = _context.Release
                .Where(release => release.Codename == codename && release.Language == language);
            
            try 
            {
                var query_result = await query.FirstAsync(cancel);

                ReleaseSharedIndex shared_release = new ToReleaseSharedDetail(query_result);

                return View(shared_release);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpGet("exists/{codename}")]
        public async Task<ActionResult<bool>> ReleaseExits(
            CancellationToken cancel, 
            string codename,
            [FromQuery] string language
        )
        {
            var exists = await _context.Release
                .AnyAsync(release => release.Codename == codename && release.Language == language, cancel);

            return View(exists);
        }



        [HttpPost("admin")]
        public async Task<IActionResult> CreateRelease(
            [FromBody] ReleaseSharedEditMode release
        )
        {
            throw new NotImplementedException();
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateRelease(
            int id, 
            [FromBody] ReleaseSharedEditMode release
        )
        {
            throw new NotImplementedException();
        }

        [HttpDelete("admin/{id}")]
        public async Task<IActionResult> DeleteRelease(int id)
        {
            throw new NotImplementedException();
        }
    }
}
