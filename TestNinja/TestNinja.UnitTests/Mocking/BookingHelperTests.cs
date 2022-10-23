using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.ExternalDependencies;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class BookingHelperTests
{
    private Booking _existingBooking = null!;
    private Mock<IBookingRepository> _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _existingBooking = new()
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017, 1, 15),
            DepartureDate = DepartOn(2017, 1, 20),
            Reference = "a"
        };
        _repository = new Mock<IBookingRepository>();
        _repository.Setup<IQueryable<Booking>>(x => x.GetActiveBookings(1)).Returns(new List<Booking>() { _existingBooking }.AsQueryable());
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
            DepartureDate = Before(_existingBooking.ArrivalDate)
        }, _repository.Object);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.ArrivalDate)
        }, _repository.Object);

        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = Before(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate)
        }, _repository.Object);

        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = Before(_existingBooking.DepartureDate)
        }, _repository.Object);

        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate)
        }, _repository.Object);

        Assert.That(result, Is.EqualTo(_existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.DepartureDate),
            DepartureDate = After(_existingBooking.DepartureDate, days: 2)
        }, _repository.Object);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void OverlappingBookingsExist_BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking()
        {
            Id = 1,
            ArrivalDate = After(_existingBooking.ArrivalDate),
            DepartureDate = After(_existingBooking.DepartureDate),
            Status = "Cancelled"
        }, _repository.Object);

        Assert.That(result, Is.Empty);
    }

    private DateTime Before(DateTime dateTime, int days = 1) => dateTime.AddDays(-days);
    private DateTime After(DateTime dateTime, int days = 1) => dateTime.AddDays(days);
    private DateTime ArriveOn(int year, int month, int day) => new DateTime(year, month, day, 14, 0, 0);
    private DateTime DepartOn(int year, int month, int day) => new DateTime(year, month, day, 10, 0, 0);
}
