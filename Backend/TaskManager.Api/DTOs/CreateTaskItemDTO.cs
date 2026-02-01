using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs
{
    public class CreateTaskItemDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int AssignedUserId { get; set; }
    }
}
