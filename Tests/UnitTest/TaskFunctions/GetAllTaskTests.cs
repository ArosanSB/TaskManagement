using Application.UseCases.Tasks;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.UnitTest.TaskFunctions;

public class GetAllTasksTests
{

    public GetAllTasksTests()
    {

    }

    [Fact]
    [Trait(Traits.Category, Traits.UnitTest)]
    public async Task GetAllTasksTest_Success()
    {
        // Arrange
        Mock<ITaskReposistory> _taskRepository = new();
        IMapper _mapper = AutoMapperTestService.AddAutoMapperProfile();
        GetAllTasks sut = new(_taskRepository.Object, _mapper);
        Fixture _fixture = new Fixture();

        Guid requestID = Guid.NewGuid();
        string title = "Update Task";
        string description = "Task should be updated";
        DateTime dueDate = DateTime.Now;
        bool isCompleted = false;

        TaskItemEntity taskEntity1 = _fixture.Build<TaskItemEntity>()
            .With(t => t.Id, requestID)
            .With(t => t.Title, title)
            .With(t => t.Description, description)
            .With(t => t.DueDate, dueDate)
            .With(t => t.IsCompleted, isCompleted)
            .Create();

        TaskItemEntity taskEntity2 = _fixture.Build<TaskItemEntity>()
            .With(t => t.Id, Guid.NewGuid)
            .With(t => t.Title, title)
            .With(t => t.Description, description)
            .With(t => t.DueDate, dueDate)
            .With(t => t.IsCompleted, isCompleted)
            .Create();

        TaskItemEntity taskEntity3 = _fixture.Build<TaskItemEntity>()
            .With(t => t.Id, Guid.NewGuid())
            .With(t => t.Title, title)
            .With(t => t.Description, description)
            .With(t => t.DueDate, dueDate)
            .With(t => t.IsCompleted, isCompleted)
            .Create();

        IEnumerable<TaskItemEntity> taskEntities = new List<TaskItemEntity> { taskEntity1, taskEntity2, taskEntity3 };
        _taskRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(taskEntities);

        // Act
        IEnumerable<TaskItemDto> result = await sut.Execute();

        // Assert
        Assert.NotEmpty(result);  // Ensure the result collection isn't empty.
        Assert.Equal(taskEntities.Count(), result.Count());

        // Check if the task properties match
        foreach (var taskEntity in taskEntities)
        {
            var matchingTask = result.FirstOrDefault(r => r.Id == taskEntity.Id);
            Assert.NotNull(matchingTask);  // Ensure the task exists in the result collection
            Assert.Equal(taskEntity.Title, matchingTask.Title);
            Assert.Equal(taskEntity.Description, matchingTask.Description);
            Assert.Equal(taskEntity.DueDate, matchingTask.DueDate);
            Assert.Equal(taskEntity.IsCompleted, matchingTask.IsCompleted);
        }
    }
}
