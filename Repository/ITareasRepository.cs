using TareasApp.Data.DTO;
using TareasApp.Entities;

namespace TareasApp.Repository
{
    public interface ITareasRepository
    {
        Task<List<Tarea>> GetAllTareasAsync();
        Task<Tarea?> GetTareaById(int id);
        Task<Tarea> CreateTarea(Tarea tarea);
        Task<Tarea?> DeleteTarea(int id);
        Task<Tarea?> UpdateTarea(int id, AddTarea newTarea);
    }
}
