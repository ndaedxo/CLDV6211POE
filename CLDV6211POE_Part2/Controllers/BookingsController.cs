using CLDV6211POE_Part2.Data;
using CLDV6211POE_Part2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211POE_Part2.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();

                if (searchBy == "BookingId")
                {
                    if (int.TryParse(searchString, out int bookingId))
                    {
                        bookings = bookings.Where(b => b.BookingId == bookingId).ToList();
                    }
                }
                else if (searchBy == "EventName")
                {
                    bookings = bookings.Where(b => 
                        b.Event != null && 
                        b.Event.Name.ToLower().Contains(searchString.ToLower())
                    ).ToList();
                }
            }

            ViewData["SearchString"] = searchString;
            ViewData["SearchBy"] = searchBy;

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
            var events = await _context.Events.ToListAsync();
            var venues = await _context.Venues.ToListAsync();

            if (!events.Any() || !venues.Any())
            {
                TempData["ErrorMessage"] = "Please create at least one event and one venue before making a booking.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(events, "EventId", "Name");
            ViewData["VenueId"] = new SelectList(venues, "VenueId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
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
                var hasConflict = await CheckDoubleBooking(booking.VenueId, booking.EventId, booking.BookingDate, null);
                
                if (hasConflict)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date/time. Please choose a different date or venue.");
                    ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
                    ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Booking created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
            return View(booking);
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
                var hasConflict = await CheckDoubleBooking(booking.VenueId, booking.EventId, booking.BookingDate, booking.BookingId);
                
                if (hasConflict)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date/time. Please choose a different date or venue.");
                    ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
                    ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
                    return View(booking);
                }

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

        private async Task<bool> CheckDoubleBooking(int venueId, int eventId, DateTime bookingDate, int? excludeBookingId)
        {
            var existingBookings = await _context.Bookings
                .Where(b => b.VenueId == venueId && b.BookingDate.Date == bookingDate.Date)
                .ToListAsync();

            var timeSlot = bookingDate.TimeOfDay;
            var duration = TimeSpan.FromHours(2);

            return existingBookings.Any(b => 
                b.BookingId != (excludeBookingId ?? 0) &&
                b.BookingDate.Date == bookingDate.Date &&
                b.BookingDate.TimeOfDay <= timeSlot + duration &&
                b.BookingDate.TimeOfDay >= timeSlot - duration
            );
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
                TempData["SuccessMessage"] = "Booking deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
