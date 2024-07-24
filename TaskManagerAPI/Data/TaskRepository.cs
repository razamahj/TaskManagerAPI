
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Tests.DTO;

namespace TaskManagerAPI.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TaskRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskDTO> AddTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Models.Task> GetTaskAsync(int id)
        {
            return await _context.Tasks.Include(t => t.SubTasks).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            return await _context.Tasks.Include(t => t.SubTasks).ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
