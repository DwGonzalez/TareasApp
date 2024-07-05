using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TareasApp.Entities
{
    public class Usuario: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
