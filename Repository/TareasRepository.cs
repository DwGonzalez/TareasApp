using Microsoft.EntityFrameworkCore;
using TareasApp.Data;
using TareasApp.Data.DTO;
using TareasApp.Entities;

namespace TareasApp.Repository
{
    public class TareasRepository : ITareasRepository
    {
        AppDBContext _context;

        public TareasRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Tarea>> GetAllTareasAsync()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<Tarea?> GetTareaById(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task<Tarea> CreateTarea(Tarea tarea)
        {
            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();

            return tarea;
        }

        public async Task<Tarea?> DeleteTarea(int id)
        {
            var tarea = GetTareaById(id).Result;

            if (tarea == null)
            {
                return null;
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task<Tarea?> UpdateTarea(int id, AddTarea newTarea)
        {
            var oldTarea = GetTareaById(id).Result;

            if (oldTarea == null)
            {
                return null;
            }

            oldTarea.Title = newTarea.Title;
            oldTarea.Description = newTarea.Description;
            oldTarea.IsCompleted = newTarea.IsCompleted;

            await _context.SaveChangesAsync();

            return oldTarea;
        }
        
    }
}
