﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Models;

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
    }
}
