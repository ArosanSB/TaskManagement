using Application;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.Middleware;
using Serilog;

namespace Presentation;

public class Startup
{
    public IConfiguration Configuration { get;  }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddControllers();
        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Dependency Injection
        services.AddScoped<ITaskReposistory, TaskReposistory>();
        services.AddApplicationServices();

        // Angular
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Allow frontend origin
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
        });

        // Setting DB
        services.AddDbContext<BusinessLogicDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        
        services.AddHttpContextAccessor();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        try
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                BusinessLogicDbContext context = scope.ServiceProvider.GetRequiredService<BusinessLogicDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                logger.LogInformation("Starting database migration...");
                context.Database.Migrate();
                logger.LogInformation("Database migration completed successfully");
            }
        }
        catch (Exception ex)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "An error occurred while migrating the database");
                throw; // Re-throw to prevent application startup if migrations fail
            }
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.UseCors("AllowAngularApp");
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

}
