using TaskManagerAPI.Tests.DTO;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        Task<TaskDTO> GetTaskAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksAsync();
        Task CreateTaskAsync(TaskDTO taskDTO);
        Task UpdateTaskAsync (int id, TaskDTO taskDTO);
        Task DeleteTaskAsync(int id);   
    }
}
