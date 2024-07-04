using Microsoft.EntityFrameworkCore;
using TareasApp.Entities;

namespace TareasApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Tarea> Tareas { get; set; }
    }
}
