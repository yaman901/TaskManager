namespace TaskManager.Api.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } // Optional for easier display
    }
}
