using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type>? types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<>))).ToList();
            foreach (Type type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("MappingProfile", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
