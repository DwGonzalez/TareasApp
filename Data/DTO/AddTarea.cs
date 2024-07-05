namespace TareasApp.Data.DTO
{
    public class AddTarea
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
