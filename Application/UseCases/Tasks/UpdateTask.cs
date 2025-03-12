using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.Tasks
{
    public record UpdateTaskRequest(TaskItemDto TaskItem);
    public class UpdateTask : IUseCase<UpdateTaskRequest, ResponseDto>
    {
        private readonly ITaskReposistory _taskRepository;
        private readonly IMapper _mapper;
        public UpdateTask(ITaskReposistory taskReposistory, IMapper mapper)
        {
            _taskRepository = taskReposistory;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Execute(UpdateTaskRequest request)
        {
            try
            {
                await _taskRepository.UpdateAsync(_mapper.Map<TaskItemEntity>(request.TaskItem));
                return new ResponseDto { IsSuccess = true, Message = "Task: " + taskItem.Title + " has been created!" };
            }
            catch
            {
                //TODO log error + throw expection
                return new ResponseDto { IsSuccess = false, Message = "Task: " + request.TaskItem.Title + " has not been created!" };

            }
        }
    }

}
