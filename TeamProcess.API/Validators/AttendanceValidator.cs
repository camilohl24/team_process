using FluentValidation;
using TeamProcess.API.DTOs;
using TeamProcess.API.Models;

namespace TeamProcess.API.Validators;

public class AttendanceValidator : AbstractValidator<AttendanceRequestDto>
{
    public AttendanceValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Employee ID must be greater than 0.");
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
        RuleFor(x => x.Status)
            .Must(s => Enum.IsDefined(typeof(AttendanceStatus), s))
            .WithMessage("Status must be a valid value.");
    }
}