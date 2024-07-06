using TareasApp.Data.DTO;
using TareasApp.Entities;

namespace TareasApp.Repository
{
    public interface ITareasRepository
    {
        Task<List<Tarea>> GetAllTareasAsync();
        Task<List<Tarea>> GetAllUserTareas(string userId);
        Task<Tarea?> GetTareaById(int id); // comparo que la tarea devuelta sea la que realmente pedi
        Task<Tarea> CreateTarea(Tarea tarea); // compraro que se agrego al listo la tarea que acabo de agregar
        Task<Tarea?> DeleteTarea(int id); // compraro que se elimino  al listo la tarea que acabo de agregar
        Task<Tarea?> UpdateTarea(int id, AddTarea newTarea); //compraro que se actualizo al listo la tarea que acabo de agregar
    }
}
