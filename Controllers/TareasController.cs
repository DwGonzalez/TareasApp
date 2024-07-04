using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TareasApp.Entities;

namespace TareasApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        public TareasController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            var tareas = new List<Tarea>
            {
                new Tarea {
                    Id = 1,
                    Title ="Mi primera tarea",
                }
            };

            return Ok(tareas);
        }
    }
}
