
namespace EmployeeManagementAPI.Services;

public class EmployeeService
{
    private readonly List<Employee> _employees = new()
    {
        new Employee(1, "Samuel", "IT"),
        new Employee(2, "Grace", "HR"),
        new Employee(3, "David", "Finance")
    };

    public List<Employee> GetAll()
    {
        return _employees;
    }

    public Employee? GetById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }
}

public record Employee(
    int Id,
    string Name,
    string Department
);
