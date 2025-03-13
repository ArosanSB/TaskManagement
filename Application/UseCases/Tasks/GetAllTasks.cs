using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases.Tasks;

public class GetAllTasks : IUseCase<IEnumerable<TaskItemDto>>
{
    private readonly ITaskReposistory _taskRepository;
    private readonly IMapper _mapper;
    public GetAllTasks(ITaskReposistory taskReposistory, IMapper mapper)
    {
        _taskRepository = taskReposistory;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TaskItemDto>> Execute()
    {
        IEnumerable<TaskItemEntity> taskItems = await _taskRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskItemDto>>(taskItems).ToList();
    }
}
