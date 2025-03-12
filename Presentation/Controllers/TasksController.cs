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
        private readonly IUseCase<DeleteTaskRequest, ResponseDto> _deleteTaskUseCase;
        private readonly IUseCase<IEnumerable<GetAllTasksRequest>> _getAllTasks;
        private readonly IUseCase<GetTaskByIDRequest, TaskItemDto> _getTaskByID;
        private readonly IUseCase<UpdateTaskRequest, ResponseDto> _updateTask;
        public TasksController(IUseCase<CreateTaskRequest, ResponseDto> useCase, 
            IUseCase<DeleteTaskRequest, ResponseDto> deleteTaskUseCase,
            IUseCase<IEnumerable<GetAllTasksRequest>> getAllTasks,
            IUseCase<GetTaskByIDRequest, TaskItemDto> getTaskByID,
            IUseCase<UpdateTaskRequest, ResponseDto> updateTask)
        {
            _createTaskUseCase = useCase;
            _deleteTaskUseCase = deleteTaskUseCase;
            _getAllTasks = getAllTasks;
            _getTaskByID = getTaskByID;
            _updateTask = updateTask;
        }

        [HttpPost("/createtask")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest taskItemDto)
        {
            ResponseDto response = await _createTaskUseCase.Execute(taskItemDto);
            return Ok(response);
        }

        [HttpDelete("/deletetask")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask([FromBody] DeleteTaskRequest taskItemDto)
        {
            ResponseDto response = await _deleteTaskUseCase.Execute(taskItemDto);
            return Ok(response);
        }

        [HttpDelete("/updatetask")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask([FromBody] TaskItemDto taskItemDto)
        {
            ResponseDto response = await _updateTask.Execute(new UpdateTaskRequest(taskItemDto));
            return Ok(response);
        }

        [HttpDelete("/getallTasks")]
        [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTasks()
        {
            IEnumerable<TaskItemDto> tasks = await _getAllTasks.Execute();
            return Ok(tasks);
        }

        [HttpDelete("/getTaskByID")]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaskByID([FromBody] Guid id)
        {
            TaskItemDto task = await _getTaskByID.Execute(new GetTaskByIDRequest(id));
            return Ok(task);
        }

    }
}
