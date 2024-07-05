namespace TareasApp.Entities
{
    public class Tarea
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public Boolean IsCompleted { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? UsuarioId { get; set; }
    }
}
