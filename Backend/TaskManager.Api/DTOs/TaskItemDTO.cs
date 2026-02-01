namespace TaskManager.Api.DTOs
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; } // Optional

        public int AssignedUserId { get; set; }
        public string AssignedUserName { get; set; } // Optional
    }
}
