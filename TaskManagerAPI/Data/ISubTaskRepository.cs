using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public interface ISubTaskRepository
    {
        Task<SubTask> GetSubTaskAsync(int id);
        Task<IEnumerable<SubTask>> GetSubTasksByTaskIdAsync(int taskId);
        System.Threading.Tasks.Task AddSubTaskAsync(SubTask subTask);
        System.Threading.Tasks.Task UpdateSubTaskAsync (SubTask subTask);
        System.Threading.Tasks.Task DeleteSubTaskAsync (int id);
    }
}
