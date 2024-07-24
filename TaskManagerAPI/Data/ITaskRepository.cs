using TaskManagerAPI.Models;
using TaskManagerAPI.Tests.DTO;

namespace TaskManagerAPI.Data
{
    public interface ITaskRepository
    {
        Task<TaskDTO> AddTask(Models.Task task);
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<Models.Task> GetTaskAsync(int id);
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        System.Threading.Tasks.Task AddTaskAsync(Models.Task task);
        System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task);
        System.Threading.Tasks.Task DeleteTaskAsync(int id);
    }
}
