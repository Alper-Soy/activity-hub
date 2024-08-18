using Application.Activities.Commands.CreateActivity;
using Application.Activities.Queries.GetActivities;
using Application.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt => { opt.UseSqlite(config.GetConnectionString("DefaultConnection")); });
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"); });
        });
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(typeof(GetActivitiesHandler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();

        return services;
    }
}
