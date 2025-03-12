using Application.Dto;
using Application.Interfaces;
using Application.UseCases.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private readonly IUseCase<CreateTaskRequest, ResponseDto> _createTaskUseCase;
        public TasksController(IUseCase<CreateTaskRequest, ResponseDto> useCase)
        {
            _createTaskUseCase = useCase;
        }

        [HttpPost("/createTask")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTask([FromBody] TaskItemDto taskItemDto)
        {
            ResponseDto response = await _createTaskUseCase.Execute(new CreateTaskRequest(taskItemDto));
            return Ok(response);
        }
    }
}
