using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tasks
{
    public record CreateTaskRequest(TaskItemDto TaskItem);
    public class CreateTask : IUseCase<CreateTaskRequest, ResponseDto>
    {
        private readonly ITaskReposistory _taskRepository;
        private readonly IMapper _mapper;

        public CreateTask(ITaskReposistory taskReposistory, IMapper mapper)
        {
            _taskRepository = taskReposistory;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Execute(CreateTaskRequest request)
        {
            try
            {
                await _taskRepository.AddAsync(_mapper.Map<TaskItemEntity>(request.TaskItem));
                return new ResponseDto { IsSuccess = true, Message = "Task: " + request.TaskItem.Title + " has been created!" };
            }
            catch
            {
                return new ResponseDto { IsSuccess = false, Message = "Task: " + request.TaskItem.Title + " has not been created!" };
            }
        }
    }
}
