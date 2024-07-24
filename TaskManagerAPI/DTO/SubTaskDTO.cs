namespace TaskManagerAPI.DTO
{
    public class SubTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
    }
}
