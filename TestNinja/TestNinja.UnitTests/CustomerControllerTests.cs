using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _customerController = null!;

    [SetUp]
    public void SetUp()
    {
        _customerController = new();
    }

    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _customerController.GetCustomer(0);

        // Exactly type
        Assert.That(result, Is.TypeOf<NotFound>());

        // Type or one of its derivatives
        Assert.That(result, Is.InstanceOf<NotFound>());
    }

    [Test]
    public void GetCustomer_IdIsNotZero_ReturnOk()
    {
        var result = _customerController.GetCustomer(1);

        Assert.That(result, Is.TypeOf<Ok>());
    }
}
