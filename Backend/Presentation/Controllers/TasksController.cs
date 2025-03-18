using Application.Dto;
using Application.Interfaces;
using Application.UseCases.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private readonly IUseCase<CreateTaskRequest, ResponseDto> _createTaskUseCase;
    private readonly IUseCase<DeleteTaskRequest, ResponseDto> _deleteTaskUseCase;
    private readonly IUseCase<IEnumerable<TaskItemDto>> _getAllTasks;
    private readonly IUseCase<GetTaskByIDRequest, TaskItemDto> _getTaskByID;
    private readonly IUseCase<UpdateTaskRequest, ResponseDto> _updateTask;
    private readonly IUseCase<SetIsCompletedRequest, ResponseDto> _setIsCompleted;

    public TasksController(IUseCase<CreateTaskRequest, ResponseDto> useCase,
        IUseCase<DeleteTaskRequest, ResponseDto> deleteTaskUseCase,
        IUseCase<IEnumerable<TaskItemDto>> getAllTasks,
        IUseCase<GetTaskByIDRequest, TaskItemDto> getTaskByID,
        IUseCase<UpdateTaskRequest, ResponseDto> updateTask,
        IUseCase<SetIsCompletedRequest, ResponseDto> setIsCompleted)
    {
        _createTaskUseCase = useCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _getAllTasks = getAllTasks;
        _getTaskByID = getTaskByID;
        _updateTask = updateTask;
        _setIsCompleted = setIsCompleted;
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

    [HttpDelete("/deletetask/{id}")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        ResponseDto response = await _deleteTaskUseCase.Execute(new DeleteTaskRequest(id));
        return Ok(response);
    }

    [HttpPut("/updatetask")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTask([FromBody] TaskItemDto taskItemDto)
    {
        ResponseDto response = await _updateTask.Execute(new UpdateTaskRequest(taskItemDto));
        return Ok(response);
    }

    [HttpGet("/getallTasks")]
    [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<TaskItemDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTasks()
    {
        IEnumerable<TaskItemDto> tasks = await _getAllTasks.Execute();
        return Ok(tasks);
    }

    [HttpGet("/getTaskByID/{id}")]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(TaskItemDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTaskByID(Guid id)
    {
        TaskItemDto task = await _getTaskByID.Execute(new GetTaskByIDRequest(id));
        return Ok(task);
    }

    [HttpPut("/setisCompleted")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SetIsCompleted([FromBody] SetIsCompletedRequest request)
    {
        ResponseDto response = await _setIsCompleted.Execute(request);
        return Ok(response);
    }
}
