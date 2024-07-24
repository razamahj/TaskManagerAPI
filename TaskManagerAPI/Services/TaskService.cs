using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskManagerAPI.Data;
using TaskManagerAPI.Tests.DTO;

namespace TaskManagerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository, ISubTaskRepository subTaskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            _mapper = mapper;
        }
        public async Task CreateTaskAsync(TaskDTO taskDTO)
        {
            var task = _mapper.Map<Models.Task>(taskDTO);
            await _taskRepository.AddTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            if(task != null)
            {
                var subtasks = await _subTaskRepository.GetSubTasksByTaskIdAsync(id);
                if (subtasks.Any())
                {
                    throw new InvalidOperationException("Cannot delete tasks with subtasks");
                }
                await _taskRepository.DeleteTaskAsync(id);
            }
        }

        public async Task<TaskDTO> GetTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetTasksAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);  
        }

        public async Task UpdateTaskAsync(int id, TaskDTO taskDTO)
        {
            var existingTask = await _taskRepository.GetTaskAsync(id);
            if (existingTask != null)
            {
                var subTasks = await _subTaskRepository.GetSubTasksByTaskIdAsync(id);
                if(taskDTO.IsComplete && subTasks.Any(st => !st.IsComplete))
                {
                    throw new InvalidOperationException("Cannot mark the task as complete because not all subtasks are complete");
                }
                _mapper.Map(taskDTO, existingTask);
                await _taskRepository.UpdateTaskAsync(existingTask);
            }
        }
    }
}
