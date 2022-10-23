using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.ExternalDependencies;

public interface IBookingRepository
{
    IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
}

internal class BookingRepository : IBookingRepository
{
    public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
    {
        var unitOfWork = new UnitOfWork();
        var bookings = unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

        if (excludedBookingId is not null)
            bookings = bookings.Where(b => b.Id != excludedBookingId);

        return bookings;
    }
}
