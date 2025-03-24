using System.Net.Http.Json;
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

public class UpdateTaskTests : IClassFixture<TestEnvironmnet>
{
    private readonly Fixture _fixture;
    private readonly TestEnvironmnet _testEnvironment;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public UpdateTaskTests(TestEnvironmnet testEnvironmnet)
    {
        _fixture = new Fixture();
        _testEnvironment = testEnvironmnet;
        _testEnvironment.CleanDB();
    }

    [Fact]
    [Trait(Traits.Category, Traits.IntegrationTest)]
    public async Task UpdateTaskTest_Success()
    {
        using (IServiceScope scope = _testEnvironment.CreateScope())
        {
            // Arrange 
            IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            BusinessLogicDbContext context = scope.ServiceProvider.GetRequiredService<BusinessLogicDbContext>();
            Guid requestID = await CreateTask(mapper, context);

            TaskItemDto request = new()
            {
                Id = requestID,
                Title = "Integration Test Task Updated",
                Description = "Testing API Updated",
                DueDate = DateTime.Now,
                IsCompleted = true
            };

            // Act
            HttpResponseMessage response = await _testEnvironment.HttpClient.PutAsJsonAsync($"/tasks/updatetask", request);

            // Assert
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ResponseDto? result = JsonSerializer.Deserialize<ResponseDto>(content, _jsonSerializerOptions);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            HttpResponseMessage responseMessage = await _testEnvironment.HttpClient.GetAsync($"/tasks/getTaskByID/{requestID}");
            string content2 = await responseMessage.Content.ReadAsStringAsync();
            TaskItemDto? taskItemDto = JsonSerializer.Deserialize<TaskItemDto>(content2, _jsonSerializerOptions);
            Assert.Equal(request.Title, taskItemDto?.Title);
            Assert.Equal(request.Description, taskItemDto?.Description);
            Assert.Equal(request.DueDate, taskItemDto?.DueDate);
            Assert.Equal(request.IsCompleted, taskItemDto?.IsCompleted);
        }
    }

    private static async Task<Guid> CreateTask(IMapper mapper, BusinessLogicDbContext context)
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
        return task.Id;
    }
}
