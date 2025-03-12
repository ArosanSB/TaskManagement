using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.UseCases.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUseCase<CreateTaskRequest, ResponseDto>, CreateTask>();

            return services;
        }
    }
}
