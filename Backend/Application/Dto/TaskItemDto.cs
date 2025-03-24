using System;
using Application.Mappers;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto;

public class TaskItemDto : IMap<TaskItemEntity>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    internal static void MappingProfile(Profile profile)
    {
        profile.CreateMap<TaskItemEntity, TaskItemDto>().ReverseMap();
    }
}
