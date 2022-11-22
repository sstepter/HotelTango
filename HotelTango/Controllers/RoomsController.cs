using HotelTango.Data;
using HotelTango.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelTango.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Id" : "";
            ViewData["RoomNumberSortParam"] = String.IsNullOrEmpty(sortOrder) ? "RoomNumber" : "";
            ViewData["RoomTypeNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "RoomTypeName" : "";
            ViewData["BedTypeSortParam"] = String.IsNullOrEmpty(sortOrder) ? "BedType" : "";
            ViewData["NumberOfBeds"] = String.IsNullOrEmpty(sortOrder) ? "NumberOfBeds" : "";
            ViewData["RoomRateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "RoomRate" : "";
            ViewData["CurrentFilter"] = searchString;

            var Rooms = from s in _context.Room.Include(r => r.RoomType)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Rooms = Rooms.Where(s => s.Id.ToString().Contains(searchString) || s.RoomNumber.ToString().Contains(searchString) || s.RoomTypeID.ToString().Contains(searchString) || s.RoomType.RoomTypeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id":
                    Rooms = Rooms.OrderByDescending(s => s.Id);
                    break;
                case "RoomNumber":
                    Rooms = Rooms.OrderByDescending(s => s.RoomNumber);
                    break;
                case "RoomTypeName":
                    Rooms = Rooms.OrderByDescending(s => s.RoomType.RoomTypeName);
                    break;
                case "BedType":
                    Rooms = Rooms.OrderBy(s => s.RoomType.BedType);
                    break;
                case "NumberOfBeds":
                    Rooms = Rooms.OrderBy(s => s.RoomType.NumberOfBeds);
                    break;
                case "RoomRate":
                    Rooms = Rooms.OrderBy(s => s.RoomType.RoomRate);
                    break;
                default:
                    Rooms = Rooms.OrderBy(s => s.Id);
                    break;
            }
            return View(await Rooms.AsNoTracking().ToListAsync());
        }


        // GET: Rooms/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id");
            return View();
        }

        // POST: Rooms/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomNumber,RoomTypeID")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", room.RoomTypeID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", room.RoomTypeID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomNumber,RoomTypeID")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", room.RoomTypeID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
