using System.Net.Http.Json;
using System.Text.Json;
using Application.Dto;
using Application.UseCases.Tasks;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTest.Utility;
using Xunit;

namespace Tests.IntegrationTest.TaskFunctions;

public class GetTaskByIDTests : IClassFixture<TestEnvironmnet>
{
    private readonly Fixture _fixture;
    private readonly TestEnvironmnet _testEnvironment;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public GetTaskByIDTests(TestEnvironmnet testEnvironmnet)
    {
        _fixture = new Fixture();
        _testEnvironment = testEnvironmnet;
        _testEnvironment.CleanDB();
    }

    [Fact]
    [Trait(Traits.Category, Traits.IntegrationTest)]
    public async Task GetTaskByIDTest_Success()
    {
        using (IServiceScope scope = _testEnvironment.CreateScope())
        {
            // Arrange 
            IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            BusinessLogicDbContext context = scope.ServiceProvider.GetRequiredService<BusinessLogicDbContext>();
            TaskItemDto request = await CreateTask(mapper, context);

            // Act
            HttpResponseMessage result = await _testEnvironment.HttpClient.GetAsync($"/tasks/getTaskByID/{request.Id}");

            // Assert
            Assert.NotNull(result);
            string content = await result.Content.ReadAsStringAsync();
            TaskItemDto? taskItemDto = JsonSerializer.Deserialize<TaskItemDto>(content, _jsonSerializerOptions);
            Assert.Equal(request.Title, taskItemDto?.Title);
            Assert.Equal(request.Description, taskItemDto?.Description);
            Assert.Equal(request.DueDate, taskItemDto?.DueDate);
            Assert.Equal(request.IsCompleted, taskItemDto?.IsCompleted);
        }
    }

    private static async Task<TaskItemDto> CreateTask(IMapper mapper, BusinessLogicDbContext context)
    {
        TaskItemEntity newTask = new()
        {
            Id = Guid.NewGuid(),
            Title = "Integration Test Task",
            Description = "Testing API",
            DueDate = DateTime.Now,
            IsCompleted = false
        };
        await context.Tasks.AddAsync(newTask);
        await context.SaveChangesAsync();
        TaskItemDto task = mapper.Map<TaskItemDto>(newTask);
        return task;
    }
}
