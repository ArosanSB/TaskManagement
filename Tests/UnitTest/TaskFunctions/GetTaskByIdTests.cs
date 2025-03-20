using Application.UseCases.Tasks;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class GetTaskByIdTests
{
    private readonly Mock<ITaskReposistory> _taskRepository;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public GetTaskByIdTests()
    {
        _taskRepository = new Mock<ITaskReposistory>();
        _mapper = AutoMapperTestService.AddAutoMapperProfile();
        _fixture = new Fixture();

    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task GetTaskByIdTest_Success()
    {
        // Arrange
        Guid requestID = Guid.NewGuid();
        string title = "Update Task";
        string description = "Task should be updated";
        DateTime dueDate = DateTime.Now;
        bool isCompleted = false;

        TaskItemEntity taskEntity = _fixture.Build<TaskItemEntity>()
            .With(t => t.Id, requestID)
            .With(t => t.Title, title)
            .With(t => t.Description, description)
            .With(t => t.DueDate, dueDate)
            .With(t => t.IsCompleted, isCompleted)
            .Create();

        GetTaskByID sut = new GetTaskByID(_taskRepository.Object, _mapper);

        // Act
        _taskRepository.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<Guid>())).ReturnsAsync(taskEntity);
        var updatedTask = await sut.Execute(new GetTaskByIDRequest(taskEntity.Id));

        // Assert
        Assert.Equal(title, updatedTask.Title);
        Assert.Equal(description, updatedTask.Description);
        Assert.Equal(dueDate, updatedTask.DueDate);
        Assert.Equal(isCompleted, updatedTask.IsCompleted);
    }
}
