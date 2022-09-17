using Moq;
using NUnit.Framework;
using TestNinja.ExternalDependencies;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class EmployeeControllerTests
{
    private Mock<IEmployeeStorage> _employeeStorage = null!;
    private EmployeeController _employeeController = null!;

    [SetUp]
    public void SetUp()
    {
        _employeeStorage = new Mock<IEmployeeStorage>();
        _employeeController = new EmployeeController(_employeeStorage.Object);
    }

    [Test]
    public void DeleteEmployee_WhenCalled_DeleteEmployee()
    {
        _employeeController.DeleteEmployee(1);

        _employeeStorage.Verify(x => x.Delete(1));
    }

    [Test]
    public void DeleteEmployee_WhenCalled_ReturnRedirectResult()
    {
        var result = _employeeController.DeleteEmployee(1);

        Assert.That(result, Is.TypeOf<RedirectResult>());
    }
}
