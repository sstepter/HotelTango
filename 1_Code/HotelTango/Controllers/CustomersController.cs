using HotelTango.Data;
using HotelTango.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelTango.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IDSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Id" : "";
            ViewData["FirstNameSortParam"] = String.IsNullOrEmpty(sortOrder)  ? "FirstName" : "";
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "LastName" : "";            
            ViewData["AddressSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Address" : "";
            ViewData["CitySortParam"] = String.IsNullOrEmpty(sortOrder) ? "City" : "";
            ViewData["StateSortParam"] = String.IsNullOrEmpty(sortOrder) ? "State" : "";
            ViewData["PostalCodeSortParam"] = String.IsNullOrEmpty(sortOrder) ? "PostalCode" : "";
            ViewData["EmailAddressSortParam"] = String.IsNullOrEmpty(sortOrder) ? "EmailAddress" : "";
            ViewData["PhoneNumberSortParam"] = String.IsNullOrEmpty(sortOrder) ? "PhoneNumber" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "dateDesc" : "date";
            ViewData["CurrentFilter"] = searchString;

            var Customers = from s in _context.Customer
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Customers = Customers.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id":
                    Customers = Customers.OrderByDescending(s => s.Id.ToString());
                    break;
                case "FirstName":
                    Customers = Customers.OrderBy(s => s.FirstName);
                    break;
                case "LastName":
                    Customers = Customers.OrderBy(s => s.LastName);
                    break;
                case "Address":
                    Customers = Customers.OrderBy(s => s.Address);
                    break;
                case "City":
                    Customers = Customers.OrderBy(s => s.City);
                    break;
                case "State":
                    Customers = Customers.OrderBy(s => s.State);
                    break;
                case "PostalCode":
                    Customers = Customers.OrderBy(s => s.PostalCode);
                    break;
                case "EmailAddress":
                    Customers = Customers.OrderBy(s => s.EmailAddress);
                    break;
                case "PhoneNumber":
                    Customers = Customers.OrderBy(s => s.PhoneNumber);
                    break;
                default:
                    Customers = Customers.OrderBy(s => s.Id);
                    break;
            }
            return View(await Customers.AsNoTracking().ToListAsync());
        }

        // GET: Customers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,City,State,PostalCode,EmailAddress,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,City,State,PostalCode,EmailAddress,PhoneNumber")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
