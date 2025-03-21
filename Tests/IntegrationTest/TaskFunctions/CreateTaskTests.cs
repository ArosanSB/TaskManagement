using System.Net.Http.Json;
using System.Text.Json;
using Application.Dto;
using Application.UseCases.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTest.Utility;
using Xunit;

namespace Tests.IntegrationTest.TaskFunctions;

public class CreateTaskTests : IClassFixture<TestEnvironmnet>
{
    private readonly Fixture _fixture;
    private readonly TestEnvironmnet _testEnvironment;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public CreateTaskTests(TestEnvironmnet testEnvironmnet)
    {
        _fixture = new Fixture();
        _testEnvironment = testEnvironmnet;
        _testEnvironment.CleanDB();
    }

    [Fact]
    [Trait(Traits.Category, Traits.IntegrationTest)]
    public async Task CreateTaskTest_Success()
    {
        using (IServiceScope scope = _testEnvironment.CreateScope())
        {
            // Arrange 
            IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            CreateTaskRequest newTask = new("Integration Test Task", "Testing API", DateTime.Now, false);

            // Act
            HttpResponseMessage response = await _testEnvironment.HttpClient.PostAsJsonAsync($"/tasks/createtask", newTask);
            
            // Assert
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            ResponseDto? result = JsonSerializer.Deserialize<ResponseDto>(content, _jsonSerializerOptions);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

        }
    }
}
