using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.Tasks;

public record SetIsCompletedRequest(Guid id, bool isCompleted);
public class SetIsCompleted : IUseCase<SetIsCompletedRequest, ResponseDto>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;
    public SetIsCompleted(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }
    public async Task<ResponseDto> Execute(SetIsCompletedRequest request)
    {
        try
        {
            await _taskRepository.SetIsCompleted(request.id, request.isCompleted);
            return new ResponseDto { IsSuccess = true, Message = "Task has been updated!" };
        }
        catch
        {
            //TODO log error + throw expection
            return new ResponseDto { IsSuccess = false, Message = "Task has not been updated!" };

        }
    }
}
