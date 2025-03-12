using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.Tasks;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Application
{
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

            return services;
        }
    }
}
