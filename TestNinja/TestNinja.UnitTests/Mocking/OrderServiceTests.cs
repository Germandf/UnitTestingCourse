using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IStorage> _storage = null!;
    private OrderService _orderService = null!;

    [SetUp]
    public void SetUp()
    {
        _storage = new Mock<IStorage>();
        _orderService = new OrderService(_storage.Object);
    }

    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var order = new Order();

        _orderService.PlaceOrder(order);

        _storage.Verify(s => s.Store(order));
    }
}
