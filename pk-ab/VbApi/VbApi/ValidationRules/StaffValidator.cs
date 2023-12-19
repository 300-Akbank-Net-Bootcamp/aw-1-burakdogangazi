using System.Text.RegularExpressions;
using FluentValidation;
using VbApi.Controllers;

namespace VbApi.ValidationRules;

public class StaffValidator : AbstractValidator<Staff>
{
    public StaffValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(10, 250).WithMessage("Name must be between 10 and 250 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email address is not valid.");

        RuleFor(p => p.Phone)
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

        RuleFor(x => x.HourlySalary)
            .InclusiveBetween(30, 400).WithMessage("Hourly salary must be between 30 and 400.");
    }
}