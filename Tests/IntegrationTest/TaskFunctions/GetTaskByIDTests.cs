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
            HttpResponseMessage response = await _testEnvironment.HttpClient.GetAsync($"/tasks/getTaskByID/{request.Id}");

            // Assert
            Assert.NotNull(response);
            string content = await response.Content.ReadAsStringAsync();
            TaskItemDto? result = JsonSerializer.Deserialize<TaskItemDto>(content, _jsonSerializerOptions);
            Assert.Equal(request.Title, result?.Title);
            Assert.Equal(request.Description, result?.Description);
            Assert.Equal(request.DueDate, result?.DueDate);
            Assert.Equal(request.IsCompleted, result?.IsCompleted);
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
