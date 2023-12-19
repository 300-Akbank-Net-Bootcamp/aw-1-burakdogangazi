using Microsoft.AspNetCore.Mvc;
using VbApi.ValidationRules;

namespace VbApi.Controllers;

public class Employee
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public double? HourlySalary { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    public EmployeeController()
    {
    }

    [HttpPost]
    public IActionResult Post([FromBody] Employee value)
    {
        EmployeeValidator employeeValidator = new EmployeeValidator();

        var validationResult = employeeValidator.Validate(value);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(value);
    }
}