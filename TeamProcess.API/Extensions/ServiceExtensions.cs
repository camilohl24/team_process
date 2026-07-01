using FluentValidation;
using FluentValidation.AspNetCore;
using TeamProcess.API.Services;
using TeamProcess.API.Validators;

namespace TeamProcess.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<DepartmentService>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<AttendanceService>();
        services.AddScoped<EntityValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();

        return services;

    }
}
