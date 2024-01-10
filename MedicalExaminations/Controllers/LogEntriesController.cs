﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalExaminations.Models;

namespace MedicalExaminations.Controllers
{
    public class LogEntriesController : Controller
    {
        private readonly AppDbContext _context;

        public LogEntriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LogEntries
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LogEntries.Include(l => l.ActionType).Include(l => l.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LogEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LogEntries == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries
                .Include(l => l.ActionType)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // GET: LogEntries/Create
        public IActionResult Create()
        {
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: LogEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Timestamp,ActionTypeId,ObjectId,ObjectAttributes,FileId")] LogEntry logEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id", logEntry.ActionTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", logEntry.UserId);
            return View(logEntry);
        }

        // GET: LogEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LogEntries == null)
            {
                return NotFound();
            }

            var logEntry = await _context.LogEntries
                .Include(l => l.ActionType)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logEntry == null)
            {
                return NotFound();
            }

            return View(logEntry);
        }

        // POST: LogEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LogEntries == null)
            {
                return Problem("Entity set 'AppDbContext.LogEntries'  is null.");
            }
            var logEntry = await _context.LogEntries.FindAsync(id);
            if (logEntry != null)
            {
                _context.LogEntries.Remove(logEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogEntryExists(int id)
        {
          return (_context.LogEntries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
