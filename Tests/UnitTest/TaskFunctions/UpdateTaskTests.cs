﻿using Application.Dto;
using Application.UseCases.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class UpdateTaskTests
{
    private readonly Mock<ITaskReposistory> _taskRepository;
    private readonly IMapper _mapper;
    private readonly UpdateTask _taskUseCase;
    private readonly GetTaskByID _getTaskByIdUseCase;

    public UpdateTaskTests()
    {
        _taskRepository = new Mock<ITaskReposistory>();
        _mapper = AutoMapperTestService.AddAutoMapperProfile();
        _taskUseCase = new UpdateTask(_taskRepository.Object, _mapper);
        _getTaskByIdUseCase = new GetTaskByID(_taskRepository.Object, _mapper);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task UpdateTaskTest_Success()
    {
        // Arrange
        Guid requestID = Guid.NewGuid();
        string title = "Update Task";
        string description = "Task should be updated";
        DateTime dueDate = DateTime.Now;
        bool isCompleted = false;

        TaskItemEntity taskEntity = new TaskItemEntity
        {
            Id = requestID,
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = isCompleted
        };

        TaskItemDto request = new TaskItemDto
        {
            Id = requestID,
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = isCompleted
        };

        // Act
        _taskRepository.Setup(repo => repo.UpdateAsync(taskEntity));
        var response = await _taskUseCase.Execute(new UpdateTaskRequest(request));

        // Assert
        Assert.True(response.IsSuccess);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task UpdateTaskTest_Fail()
    {
        // Arrange
        Guid requestID = Guid.NewGuid();
        string title = "";
        string description = "Task should be updated";
        DateTime dueDate = DateTime.Now;
        bool isCompleted = false;

        TaskItemDto request = new TaskItemDto
        {
            Id = requestID,
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = isCompleted
        };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _taskUseCase.Execute(new UpdateTaskRequest(request)));

        // Assert
        Assert.Equal("Title", exception.ParamName);
    }
}
