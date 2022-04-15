using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Infrastructure.Persistence;
using TodoDTO.Infrastructure.Services;

namespace TodoDTO.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(TodoContext).Assembly.FullName)));
            
            services.AddScoped<ITodoContext>(provider => provider.GetService<TodoContext>());
            services.AddTransient<IDateTime, DateTimeService>();
            
            return services;
        }
    }
}