using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelTango.Data;
using HotelTango.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelTango.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Id" : "";
            ViewData["ReservationIdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "ReservationId" : "";
            ViewData["FirstNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "FirstName" : "";
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "LastName" : "";
            ViewData["RoomNumberSortParam"] = String.IsNullOrEmpty(sortOrder) ? "RoomNumber" : "";
            ViewData["WIFI_PasscodeSortParam"] = String.IsNullOrEmpty(sortOrder) ? "WIFI_Passcode" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "dateDesc" : "date";
            ViewData["CurrentFilter"] = searchString;

            var Reservations = from s in _context.Reservation.Include(r => r.Customer).Include(r => r.Room.RoomType)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Reservations = Reservations.Where(s => s.Customer.LastName.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id":
                    Reservations = Reservations.OrderByDescending(s => s.Id);
                    break;
                case "ReservationId":
                    Reservations = Reservations.OrderByDescending(s => s.Customer.Id);
                    break;
                case "FirstName":
                    Reservations = Reservations.OrderByDescending(s => s.Customer.FirstName);
                    break;
                case "LastName":
                    Reservations = Reservations.OrderByDescending(s => s.Customer.LastName);
                    break;
                case "RoomNumber":
                    Reservations = Reservations.OrderByDescending(s => s.Room.RoomNumber);
                    break;
                case "WIFI_Passcode":
                    Reservations = Reservations.OrderByDescending(s => s.WIFI_Passcode);
                    break;
                case "date":
                    Reservations = Reservations.OrderByDescending(s => s.StartDate);
                    break;
                case "date2":
                    Reservations = Reservations.OrderByDescending(s => s.EndDate);
                    break;
                default:
                    Reservations = Reservations.OrderBy(s => s.Id);
                    break;
            }
            return View(await Reservations.AsNoTracking().ToListAsync());
        }


        // GET: Reservations/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id");
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerID,RoomID,WIFI_Passcode,StartDate,EndDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customer, "Id", "Id", reservation.CustomerID);
            ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id", reservation.RoomID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize]
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
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerID,RoomID,WIFI_Passcode,StartDate,EndDate")] Reservation reservation)
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
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [Authorize]
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
