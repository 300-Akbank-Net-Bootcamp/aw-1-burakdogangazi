using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VbApi.ValidationRules;

namespace VbApi.Controllers;

public class Staff
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal? HourlySalary { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] Staff value)
    {
        StaffValidator staffValidator = new StaffValidator();

        var validationResult = staffValidator.Validate(value);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(value);
    }
}