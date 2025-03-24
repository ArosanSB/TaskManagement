using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Presentation;
using Presentation.Middleware;

namespace Tests.IntegrationTest.Utility;

public class TestEnvironmentStartup
{
    private Guid _dbName = Guid.NewGuid();

    public IConfiguration Configuration { get; }

    public TestEnvironmentStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc().AddApplicationPart(typeof(Program).Assembly).AddControllersAsServices();
        // Configure the database
        services.AddDbContext<BusinessLogicDbContext>(options =>
        {
            options.UseInMemoryDatabase(_dbName.ToString());
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            options.EnableSensitiveDataLogging(true);
        });


        services.AddControllers();
        services.AddRouting();
        services.AddSwaggerGen();
        
        services.AddScoped<ITaskReposistory, TaskReposistory>();
        services.AddApplicationServices();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();
        //app.UseMiddleware<ExceptionMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        // Comming soon! 
    }

}
