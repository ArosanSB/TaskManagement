using System.Collections.Generic;
using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IUseCase<CreateTaskRequest, ResponseDto>, CreateTask>();
        services.AddScoped<IUseCase<DeleteTaskRequest, ResponseDto>, DeleteTask>();
        services.AddScoped<IUseCase<IEnumerable<TaskItemDto>>, GetAllTasks>();
        services.AddScoped<IUseCase<GetTaskByIDRequest, TaskItemDto>, GetTaskByID>();
        services.AddScoped<IUseCase<UpdateTaskRequest, ResponseDto>, UpdateTask>();
        services.AddScoped<IUseCase<SetIsCompletedRequest, ResponseDto>, SetIsCompleted>();

        return services;
    }
}
