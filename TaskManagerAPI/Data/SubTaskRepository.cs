using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly AppDbContext _context;
        public SubTaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task AddSubTaskAsync(SubTask subTask)
        {
           _context.SubTasks.Add(subTask);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteSubTaskAsync(int id)
        {
            var subTask = await _context.SubTasks.FindAsync(id);
            if (subTask != null)
            {
                _context.SubTasks.Remove(subTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SubTask> GetSubTaskAsync(int id)
        {
            return await _context.SubTasks.FindAsync(id);
        }

        public async Task<IEnumerable<SubTask>> GetSubTasksByTaskIdAsync(int taskId)
        {
            return await _context.SubTasks.Where(st => st.TaskId == taskId).ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateSubTaskAsync(SubTask subTask)
        {
            _context.SubTasks.Update(subTask);
            await _context.SaveChangesAsync();
        }
    }
}
