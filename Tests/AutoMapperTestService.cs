using Application.Mappers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public static class AutoMapperTestService
{
    public static IMapper AddAutoMapperProfile()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        IServiceProvider provider = services.BuildServiceProvider();
        return provider.GetRequiredService<IMapper>(); ;
    }
}
