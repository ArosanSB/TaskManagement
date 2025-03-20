using Application.UseCases.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class SetIsCompletedTests
{
    private readonly Mock<ITaskReposistory> _taskRepository;
    private readonly IMapper _mapper;
    private readonly SetIsCompleted _taskUseCase;

    public SetIsCompletedTests()
    {
        _taskRepository = new Mock<ITaskReposistory>();
        _mapper = AutoMapperTestService.AddAutoMapperProfile();
        _taskUseCase = new SetIsCompleted(_taskRepository.Object, _mapper);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task SetIsCompletedTest_Success()
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
        _taskRepository.Setup(repo => repo.SetIsCompleted(It.IsAny<Guid>(),It.IsAny<bool>()));
        var response = await _taskUseCase.Execute(new SetIsCompletedRequest(requestID, true));

        // Assert
        Assert.True(response.IsSuccess);
    }

}
