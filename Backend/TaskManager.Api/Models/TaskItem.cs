namespace TaskManager.Api.Models
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int AssignedUserId { get; set; }
        public User AssignedUser { get; set; }
    }

}
