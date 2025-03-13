using System;
using System.Threading.Tasks;
using Application.Dto;
using Application.Guards;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Serilog;

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
        Guard.ThrowIfArgumentNull(request.id, nameof(request.id));
        Guard.ThrowIfArgumentNull(request.isCompleted, nameof(request.isCompleted));
        try
        {
            await _taskRepository.SetIsCompleted(request.id, request.isCompleted);
            return new ResponseDto 
            { 
                IsSuccess = true, 
                Message = $"Task: {request.id} has been updated isCompleted to {request.isCompleted}!"

            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error updating isCompleted task: {TaskTitle}", request.id);

            throw new Exception($"Failed to update isCompleted task: {request.id}", ex);
        }
    }
}
