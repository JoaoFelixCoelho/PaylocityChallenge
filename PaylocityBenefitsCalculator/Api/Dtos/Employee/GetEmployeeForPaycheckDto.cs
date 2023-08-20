namespace Api.Dtos.Employee;

public class GetEmployeeForPaycheckDto
{
    public int Id { get; set; }
    public decimal Salary { get; set; }
    public int Age { get; set; }
    public List<int> DependentsAge { get; set; } = new List<int>();
}
