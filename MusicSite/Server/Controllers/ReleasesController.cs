using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Models;
using MusicSite.Shared.SharedModels;
using MusicSite.Server.Transformations.FromDbModelToShared;
using Microsoft.AspNetCore.Authorization;

namespace MusicSite.Server.Controllers
{
    [Authorize]
    public class ReleasesController : Controller
    {
        private readonly MusicSiteServerContext _context;
        private readonly ILogger<ReleasesController> _logger;

        public ReleasesController(MusicSiteServerContext context, ILogger<ReleasesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Releases
        [HttpGet(), AllowAnonymous]
        public async Task<ActionResult<ReleaseSharedIndex[]>> IndexByAuthor(
            CancellationToken cancel, 
            string author, 
            string language, 
            int page = 0, 
            int records_per_page = 100
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

        // GET: Release
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<ReleaseSharedIndex>> GetRelease(CancellationToken cancel, string codename, string language)
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

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<bool>> ReleaseExits(CancellationToken cancel, string codename, string language)
        {
            var exists = await _context.Release
                .AnyAsync(release => release.Codename == codename && release.Language == language, cancel);

            return View(exists);
        }



        [HttpPost]
        public async Task<IActionResult> CreateRelease(ReleaseSharedEditMode release)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRelease(int id, ReleaseSharedEditMode release)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRelease(int id)
        {
            throw new NotImplementedException();
        }
    }
}
