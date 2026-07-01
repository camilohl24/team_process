using FluentValidation;
using TeamProcess.API.DTOs;
namespace TeamProcess.API.Validators;

public class EmployeeValidator : AbstractValidator<EmployeeRequestDto>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(50).WithMessage("Position must not exceed 50 characters.");
        RuleFor(x => x.EntryDate)
            .NotEmpty().WithMessage("Entry date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Entry date cannot be in the future.");
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Department ID must be greater than 0.");


    }
}
