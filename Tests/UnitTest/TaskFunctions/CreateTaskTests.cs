using Application.UseCases.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Tests;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class CreateTaskTests
{
    private readonly Mock<ITaskReposistory> _taskRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateTask _taskUseCase;

    public CreateTaskTests()
    {
        _taskRepository = new Mock<ITaskReposistory>();
        _mockMapper = new Mock<IMapper>();
        _taskUseCase = new CreateTask(_taskRepository.Object, _mockMapper.Object);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task CreateTaskTest_Success()
    {
        // Arrange
        Guid requestID = Guid.NewGuid();
        var request = new CreateTaskRequest("New Task", "Description", DateTime.Now, false);
        var taskEntity = new TaskItemEntity { Id=requestID, Title = "New Task", Description = "Task Description", DueDate = DateTime.Now, IsCompleted = false };

        _taskRepository.Setup(repo => repo.AddAsync(It.IsAny<TaskItemEntity>()));

        // Act
        var response = await _taskUseCase.Execute(request);

        // Assert
        Assert.True(response.IsSuccess);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task CreateTaskTest_Fail()
    {
        // Arrange
        var request = new CreateTaskRequest("", "Description", DateTime.Now, false);
        var taskEntity = new TaskItemEntity { Title = "", Description = "Task Description" };

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _taskUseCase.Execute(request));

        // Assert
        Assert.Equal("title", exception.ParamName);
    }
}
