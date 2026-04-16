using CLDV6211POE_Part1.Data;
using CLDV6211POE_Part1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211POE_Part1.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name");
            ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            var eventIdStr = Request.Form["EventId"];
            var venueIdStr = Request.Form["VenueId"];
            var bookingDateStr = Request.Form["BookingDate"];

            if (!int.TryParse(eventIdStr, out int eventId) || eventId == 0)
            {
                ModelState.AddModelError("EventId", "Please select an event.");
            }

            if (!int.TryParse(venueIdStr, out int venueId) || venueId == 0)
            {
                ModelState.AddModelError("VenueId", "Please select a venue.");
            }

            if (!DateTime.TryParse(bookingDateStr, out DateTime bookingDate))
            {
                ModelState.AddModelError("BookingDate", "Please select a booking date.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", eventId);
                ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", venueId);
                return View();
            }

            var eventExists = await _context.Events.AnyAsync(e => e.EventId == eventId);
            var venueExists = await _context.Venues.AnyAsync(v => v.VenueId == venueId);

            if (!eventExists)
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
                ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", eventId);
                ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", venueId);
                return View();
            }

            if (!venueExists)
            {
                ModelState.AddModelError("VenueId", "Selected venue does not exist.");
                ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", eventId);
                ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", venueId);
                return View();
            }

            var booking = new Booking
            {
                EventId = eventId,
                VenueId = venueId,
                BookingDate = bookingDate
            };

            _context.Add(booking);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Booking created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            var eventExists = await _context.Events.AnyAsync(e => e.EventId == booking.EventId);
            var venueExists = await _context.Venues.AnyAsync(v => v.VenueId == booking.VenueId);

            if (!eventExists)
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
            }

            if (!venueExists)
            {
                ModelState.AddModelError("VenueId", "Selected venue does not exist.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Booking updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
