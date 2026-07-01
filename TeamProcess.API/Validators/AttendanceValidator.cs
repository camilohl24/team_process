using FluentValidation;
using TeamProcess.API.DTOs;
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
            .NotEmpty().WithMessage("Status is required.")
            .IsInEnum().WithMessage("Status must be a valid enum value.");

    }
}
