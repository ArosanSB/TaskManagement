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

public class DeleteTaskTests : IClassFixture<TestEnvironmnet>
{
    private readonly Fixture _fixture;
    private readonly TestEnvironmnet _testEnvironment;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public DeleteTaskTests(TestEnvironmnet testEnvironmnet)
    {
        _fixture = new Fixture();
        _testEnvironment = testEnvironmnet;
        _testEnvironment.CleanDB();
    }

    [Fact]
    [Trait(Traits.Category, Traits.IntegrationTest)]
    public async Task DeleteTaskTest_Success()
    {
        using (IServiceScope scope = _testEnvironment.CreateScope())
        {
            // Arrange 
            IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            BusinessLogicDbContext context = scope.ServiceProvider.GetRequiredService<BusinessLogicDbContext>();
            Guid request = await CreateTask(mapper, context);

            // Act
            HttpResponseMessage response = await _testEnvironment.HttpClient.DeleteAsync($"/tasks/deletetask/{request}");

            // Assert
            Assert.NotNull(response);
            string content = await response.Content.ReadAsStringAsync();
            ResponseDto? result = JsonSerializer.Deserialize<ResponseDto>(content, _jsonSerializerOptions);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
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
