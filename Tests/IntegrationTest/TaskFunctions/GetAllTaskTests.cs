using System.Text.Json;
using Application.Dto;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTest.Utility;
using Xunit;

namespace Tests.IntegrationTest.TaskFunctions;

public class GetAllTaskTests : IClassFixture<TestEnvironmnet>
{
    private readonly Fixture _fixture;
    private readonly TestEnvironmnet _testEnvironment;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public GetAllTaskTests(TestEnvironmnet testEnvironmnet)
    {
        _fixture = new Fixture();
        _testEnvironment = testEnvironmnet;
        _testEnvironment.CleanDB();
    }

    [Fact]
    [Trait(Traits.Category, Traits.IntegrationTest)]
    public async Task GetAllTaskTest_Success()
    {
        using (IServiceScope scope = _testEnvironment.CreateScope())
        {
            // Arrange 
            IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            BusinessLogicDbContext context = scope.ServiceProvider.GetRequiredService<BusinessLogicDbContext>();
            IEnumerable<TaskItemDto> tasks = await CreateTask(mapper, context);

            // Act
            HttpResponseMessage response = await _testEnvironment.HttpClient.GetAsync($"/tasks/getallTasks");

            // Assert
            Assert.NotNull(response);
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<TaskItemDto>? result = JsonSerializer.Deserialize<IEnumerable<TaskItemDto>>(content, _jsonSerializerOptions);
            Assert.Equal(tasks.Count(), result?.Count());

            foreach (var taskEntity in tasks)
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

    private static async Task<IEnumerable<TaskItemDto>> CreateTask(IMapper mapper, BusinessLogicDbContext context)
    {
        List<TaskItemEntity> taskList = new();
        TaskItemEntity newTask = new()
        {
            Id = Guid.NewGuid(),
            Title = "Integration Test Task",
            Description = "Testing API",
            DueDate = DateTime.Now,
            IsCompleted = false
        };
        taskList.Add(newTask);

        newTask = new()
        {
            Id = Guid.NewGuid(),
            Title = "Integration Test Create Task",
            Description = "Testing API",
            DueDate = DateTime.Now,
            IsCompleted = false
        };
        taskList.Add(newTask);

        newTask = new()
        {
            Id = Guid.NewGuid(),
            Title = "Integration Test Delete Task",
            Description = "Testing API",
            DueDate = DateTime.Now,
            IsCompleted = false
        };
        taskList.Add(newTask);
        context.Tasks.AddRange(taskList);
        await context.SaveChangesAsync();

        IEnumerable<TaskItemDto> tasks = mapper.Map<List<TaskItemDto>>(taskList);
        return tasks;
    }
}
