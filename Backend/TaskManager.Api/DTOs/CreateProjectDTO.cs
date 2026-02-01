using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs
{
    public class CreateProjectDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
    }
}
