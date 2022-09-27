using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelTango.Data;
using HotelTango.Models;
using System.Security.Cryptography.X509Certificates;

namespace HotelTango.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservation.Include(r => r.Customer).Include(r => r.Room).Include(r => r.RoomType);
            var result = applicationDbContext.ToListAsync();
            return View(result);
        }

        public async Task<IActionResult> CheckReservationsAvailable()
        {
            var applicationDbContext = _context.Reservation.Include(r => r.Customer).Include(r => r.Room).Include(r => r.RoomType);

            var availableRooms = _context.Room.FromSqlRaw("SELECT * FROM [HotelTango].[dbo].[Room] ro WHERE NOT EXISTS (select roomID r from Reservation  r where StartDate <= GETDATE() AND EndDate >= GETDATE() and ro.Id = r.RoomID)").ToList();

            //return View(await availableRooms);
            return View("Room",await _context.Room.FromSqlRaw("SELECT * FROM [HotelTango].[dbo].[Room] ro WHERE NOT EXISTS (select roomID r from Reservation  r where StartDate <= GETDATE() AND EndDate >= GETDATE() and ro.Id = r.RoomID)").ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id");
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id");
            return View();
        }

        
        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,RoomID,StartDate,EndDate,RoomTypeID")] Reservation reservation)
        {
            var duchess = _context.Reservation.Where(x => x.Id == 1 && x.RoomID == 3).FirstOrDefault();

            
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id", reservation.RoomID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", reservation.RoomTypeID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id", reservation.RoomID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", reservation.RoomTypeID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,RoomID,StartDate,EndDate,RoomTypeID")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id", reservation.RoomID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomType, "Id", "Id", reservation.RoomTypeID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
