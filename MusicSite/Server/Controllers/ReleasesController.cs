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

namespace MusicSite.Server.Controllers
{
    public class ReleasesController : Controller
    {
        private readonly MusicSiteServerContext _context;

        public ReleasesController(MusicSiteServerContext context)
        {
            _context = context;
        }

        // GET: Releases
        public async Task<IActionResult> IndexByAuthor(string author, string language)
        {
            var query = _context.Release
                .Where(release => release.Language == language && release.Author == author);

            var query_list = await query.ToArrayAsync();

            var shared_releases = query_list.Select(
                release => new Shared.SharedModels.Release 
                {
                    Codename = release.Codename,
                    Language = release.Language,
                    Name = release.Name,
                    Type = release.Type,
                    DateRelease = release.DateRelease,
                    Author = release.Author,
                    ShortDescription = release.ShortDescription,
                    Description = release.Description,
                    DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs)
                }
            ).ToArray();

            return View(shared_releases);
        }

        public async Task<IActionResult> GetRelease(string codename, string language)
        {
            var query = _context.Release
                .Where(release => release.Codename == codename && release.Language == language);

            var query_result = await query.FirstAsync();

            var shared_release = new Shared.SharedModels.Release
            {
                Codename = query_result.Codename,
                Language = query_result.Language,
                Author = query_result.Author,
                Name = query_result.Name,
                DateRelease = query_result.DateRelease,
                Description = query_result.Description,
                ShortDescription= query_result.ShortDescription,
                Songs = query_result.ReleaseSongs.Select(
                    song => new Shared.SharedModels.ReleaseSong
                    {
                        Name = song.Name,
                        Description = song.Description,
                        Lyrics = song.Lyrics,
                        IsInstrumental = song.Lyrics is null,
                        LengthSecs = song.LengthSecs
                    }
                ).ToArray(),
                DurationInSecs = query_result.ReleaseSongs.Sum(song => song.LengthSecs)
            };

            return View(shared_release);
        }

            /*
        // GET: Releases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Release.ToListAsync());
        }

        // GET: Releases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release
                .FirstOrDefaultAsync(m => m.Id == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }

        // GET: Releases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Releases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codename,Language,Status,Name,Type,DateRelease,Author,ShortDescription,Description")] Release release)
        {
            if (ModelState.IsValid)
            {
                _context.Add(release);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(release);
        }

        // GET: Releases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }
            return View(release);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codename,Language,Status,Name,Type,DateRelease,Author,ShortDescription,Description")] Release release)
        {
            if (id != release.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(release);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReleaseExists(release.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(release);
        }

        // GET: Releases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Release
                .FirstOrDefaultAsync(m => m.Id == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var release = await _context.Release.FindAsync(id);
            _context.Release.Remove(release);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReleaseExists(int id)
        {
            return _context.Release.Any(e => e.Id == id);
        }
            */
    }
}
