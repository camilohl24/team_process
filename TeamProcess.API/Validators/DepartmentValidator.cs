using FluentValidation;
using TeamProcess.API.DTOs;
namespace TeamProcess.API.Validators;

public class DepartmentValidator : AbstractValidator<DepartmentRequestDto>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(100).WithMessage("Department name must not exceed 100 characters.");
    }
}
