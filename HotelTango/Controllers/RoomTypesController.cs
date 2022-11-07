﻿using HotelTango.Data;
using HotelTango.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelTango.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomTypes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.RoomType.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "dateDesc" : "date";
            ViewData["CurrentFilter"] = searchString;

            var RoomTypes = from s in _context.RoomType
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                RoomTypes = RoomTypes.Where(s => s.RoomTypeName.Contains(searchString) || s.BedType.Contains(searchString) || s.NumberOfBeds.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id":
                    RoomTypes = RoomTypes.OrderByDescending(s => s.Id);
                    break;
                case "nameDesc":
                    RoomTypes = RoomTypes.OrderByDescending(s => s.RoomTypeName);
                    break;
                case "BedType":
                    RoomTypes = RoomTypes.OrderByDescending(s => s.BedType);
                    break;
                case "NumberOfBeds":
                    RoomTypes = RoomTypes.OrderByDescending(s => s.NumberOfBeds);
                    break;
                case "RoomRate":
                    RoomTypes = RoomTypes.OrderByDescending(s => s.RoomRate);
                    break;
                default:
                    RoomTypes = RoomTypes.OrderBy(s => s.RoomTypeName);
                    break;
            }
            return View(await RoomTypes.AsNoTracking().ToListAsync());
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomTypeName,BedType,NumberOfBeds,RoomRate")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomTypeName,BedType,NumberOfBeds,RoomRate")] RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeExists(roomType.Id))
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
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomType = await _context.RoomType.FindAsync(id);
            _context.RoomType.Remove(roomType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
            return _context.RoomType.Any(e => e.Id == id);
        }
    }
}
