using AutoMapper;

namespace Application.Mappers;

public interface IMap<T>
{
    void MappingProfile(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
