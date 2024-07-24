using AutoMapper;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTO;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

public class SubTaskService : ISubTaskService
{
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly IMapper _mapper;

    public SubTaskService(ISubTaskRepository subTaskRepository, IMapper mapper)
    {
        _subTaskRepository = subTaskRepository;
        _mapper = mapper;
    }

    public async Task<SubTaskDTO> GetSubTaskAsync(int id)
    {
        var subTask = await _subTaskRepository.GetSubTaskAsync(id);
        return _mapper.Map<SubTaskDTO>(subTask);
    }

    public async Task<IEnumerable<SubTaskDTO>> GetSubTasksByTaskIdAsync(int taskId)
    {
        var subtasks = await _subTaskRepository.GetSubTasksByTaskIdAsync(taskId);
        return _mapper.Map<IEnumerable<SubTaskDTO>>(subtasks);
    }

    public async System.Threading.Tasks.Task CreateSubTaskAsync(SubTaskDTO subTaskDto)
    {
        var subTask = _mapper.Map<SubTask>(subTaskDto);
        await _subTaskRepository.AddSubTaskAsync(subTask);
    }

    public async System.Threading.Tasks.Task UpdateSubTaskAsync(int id, SubTaskDTO subTaskDto)
    {
        var existingSubTask = await _subTaskRepository.GetSubTaskAsync(id);
        if (existingSubTask != null)
        {
            _mapper.Map(subTaskDto, existingSubTask);
            await _subTaskRepository.UpdateSubTaskAsync(existingSubTask);
        }
    }

    public async System.Threading.Tasks.Task DeleteSubTaskAsync(int id)
    {
        await _subTaskRepository.DeleteSubTaskAsync(id);
    }
}