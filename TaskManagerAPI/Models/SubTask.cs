namespace TaskManagerAPI.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
