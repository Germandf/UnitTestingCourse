using System.Data.Entity;

namespace TestNinja.ExternalDependencies;

public interface IEmployeeStorage
{
    void Delete(int id);
}

public class EmployeeStorage : IEmployeeStorage
{
    private EmployeeContext _db;

    public EmployeeStorage(EmployeeContext db)
    {
        _db = db;
    }

    public void Delete(int id)
    {
        var employee = _db.Employees.Find(id);

        if (employee is null)
            return;

        _db.Employees.Remove(employee);
        _db.SaveChanges();
    }
}

public class EmployeeContext
{
    public DbSet<Employee> Employees { get; set; }

    public void SaveChanges()
    {
    }
}

public class Employee
{
}