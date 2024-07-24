using TaskManagerAPI.DTO;

namespace TaskManagerAPI.Services
{
    public interface ISubTaskService
    {
        Task<SubTaskDTO> GetSubTaskAsync(int id);
        Task<IEnumerable<SubTaskDTO>> GetSubTasksByTaskIdAsync(int taskId);
        Task CreateSubTaskAsync(SubTaskDTO subTaskDTO);
        Task UpdateSubTaskAsync(int id, SubTaskDTO subTaskDTO);
        Task DeleteSubTaskAsync(int id);
    }
}
