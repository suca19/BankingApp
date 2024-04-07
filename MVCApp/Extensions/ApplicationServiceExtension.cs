using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Interfaces;
using MVCApp.Services;
using CurrentUserService = MVCApp.Interfaces.CurrentUserService;
using EmployeeService = MVCApp.Interfaces.EmployeeService;
using TransactionService = MVCApp.Interfaces.TransactionService;

namespace MVCApp.Extensions;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        
        // HttpContextAccessor to get the current user logged in
        services.AddHttpContextAccessor();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Add services to the container.
        services.AddScoped<EmployeeService, EmployeeService>();
        services.AddScoped<CustomerService, CustomerService>();
        services.AddScoped<CurrentUserService, Services.CurrentUserService>();
        services.AddScoped<TransactionService, Services.TransactionService>();

        // Run migrations
        using (var serviceScope = services.BuildServiceProvider().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<DataContent>();
            context.Database.Migrate();
        }

        return services;
    }
    
}