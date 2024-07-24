namespace TaskManagerAPI.Models
{
    public class Task
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
