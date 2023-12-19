using System.Text.RegularExpressions;
using FluentValidation;
using VbApi.Controllers;

namespace VbApi.ValidationRules;

public class EmployeeValidator : AbstractValidator<Employee>
{
    private const int MinJuniorSalary = 50;
    private const int MinSeniorSalary = 200;

    public EmployeeValidator()
    {
        RuleFor(employee => employee.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(10, 250)
            .WithMessage("Invalid Name");

        RuleFor(employee => employee.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of Birth is required.")
            .Must(BeValidBirthDate)
            .WithMessage("Birthdate is not valid.");

        RuleFor(employee => employee.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email address is not valid.");

        RuleFor(employee => employee.Phone)
            .NotEmpty()
            .WithMessage("Phone is required.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
            .WithMessage("Phone is not valid.");

        RuleFor(employee => employee.HourlySalary)
            .InclusiveBetween(50, 400)
            .WithMessage("Hourly salary does not fall within allowed range.")
            .Must((employee, hourlySalary) => IsValidHourlySalary(employee, hourlySalary))
            .WithMessage("Minimum hourly salary is not valid.");
    }

    private bool BeValidBirthDate(DateTime dateOfBirth)
    {
        var minAllowedBirthDate = DateTime.Today.AddYears(-65);
        return minAllowedBirthDate <= dateOfBirth;
    }

    private bool IsValidHourlySalary(Employee employee, double? hourlySalary)
    {
        var dateBeforeThirtyYears = DateTime.Today.AddYears(-30);
        var isOlderThanThirdyYears = employee.DateOfBirth <= dateBeforeThirtyYears;

        return isOlderThanThirdyYears ? hourlySalary >= MinSeniorSalary : hourlySalary >= MinJuniorSalary;
    }
}