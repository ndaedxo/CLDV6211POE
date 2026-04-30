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
            var query = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();

                if (searchBy == "BookingId")
                {
                    if (int.TryParse(searchString, out int bookingId))
                    {
                        query = query.Where(b => b.BookingId == bookingId);
                    }
                }
                else if (searchBy == "EventName")
                {
                    query = query.Where(b =>
                        b.Event != null &&
                        EF.Functions.Like(b.Event.Name, $"%{searchString}%")
                    );
                }
            }

            var bookings = await query.ToListAsync();

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
            var events = await _context.Events.AsNoTracking().ToListAsync();
            var venues = await _context.Venues.AsNoTracking().ToListAsync();

            if (!events.Any() || !venues.Any())
            {
                TempData["ErrorMessage"] = "Please create at least one event and one venue before making a booking.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EventId = new SelectList(events, "EventId", "Name");
            ViewBag.VenueId = new SelectList(venues, "VenueId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            var events = await _context.Events.AsNoTracking().ToListAsync();
            var venues = await _context.Venues.AsNoTracking().ToListAsync();

            if (!events.Any(e => e.EventId == booking.EventId))
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
            }

            if (!venues.Any(v => v.VenueId == booking.VenueId))
            {
                ModelState.AddModelError("VenueId", "Selected venue does not exist.");
            }

            if (ModelState.IsValid)
            {
                var hasConflict = await CheckDoubleBooking(booking.VenueId, booking.EventId, null);
                
                if (hasConflict)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date/time. Please choose a different date or venue.");
                    ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
                    ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
                    return View(booking);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Booking created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
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

            var events = await _context.Events.AsNoTracking().ToListAsync();
            var venues = await _context.Venues.AsNoTracking().ToListAsync();

            ViewData["EventId"] = new SelectList(events, "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(venues, "VenueId", "Name", booking.VenueId);
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

            var events = await _context.Events.AsNoTracking().ToListAsync();
            var venues = await _context.Venues.AsNoTracking().ToListAsync();

            if (!events.Any(e => e.EventId == booking.EventId))
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
            }

            if (!venues.Any(v => v.VenueId == booking.VenueId))
            {
                ModelState.AddModelError("VenueId", "Selected venue does not exist.");
            }

            if (ModelState.IsValid)
            {
                var hasConflict = await CheckDoubleBooking(booking.VenueId, booking.EventId, booking.BookingId);
                
                if (hasConflict)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date/time. Please choose a different date or venue.");
                    ViewData["EventId"] = new SelectList(await _context.Events.ToListAsync(), "EventId", "Name", booking.EventId);
                    ViewData["VenueId"] = new SelectList(await _context.Venues.ToListAsync(), "VenueId", "Name", booking.VenueId);
                    return View(booking);
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Booking updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(events, "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        private async Task<bool> CheckDoubleBooking(int venueId, int eventId, int? excludeBookingId)
        {
            var newEvent = await _context.Events.FindAsync(eventId);
            if (newEvent == null) return false;

            var conflictingBooking = await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.VenueId == venueId)
                .Where(b => b.BookingId != (excludeBookingId ?? 0))
                .Where(b => b.Event != null)
                .Where(b => 
                    // Check if time ranges overlap: new event starts before existing ends AND new event ends after existing starts
                    (newEvent.StartDate < b.Event.EndDate && newEvent.EndDate > b.Event.StartDate)
                )
                .FirstOrDefaultAsync();

            return conflictingBooking != null;
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Booking deleted successfully.";
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
