using Application.UseCases.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class DeleteTaskTests
{
    private readonly Mock<ITaskReposistory> _taskRepository;
    private readonly IMapper _mapper;
    private readonly DeleteTask _taskUseCase;

    public DeleteTaskTests()
    {
        _taskRepository = new Mock<ITaskReposistory>();
        _mapper = AutoMapperTestService.AddAutoMapperProfile();
        _taskUseCase = new DeleteTask(_taskRepository.Object, _mapper);
    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task DeleteTaskTest_Success()
    {
        // Arrange
        Guid requestID = Guid.NewGuid();
        string title = "Task 1";
        string description = "Description 1";
        DateTime dueDate = DateTime.Now;
        bool isCompleted = false;

        TaskItemEntity task = new TaskItemEntity
        {
            Id = requestID,
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = isCompleted
        };

        _taskRepository.Setup(x => x.DeleteAsync(requestID));

        // Act
        var result = await _taskUseCase.Execute(new DeleteTaskRequest(requestID));

        // Assert
        Assert.True(result.IsSuccess);
    }
}
