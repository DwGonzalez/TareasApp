using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasApp.Data;
using TareasApp.Data.DTO;
using TareasApp.Entities;
using TareasApp.Repository;

namespace TareasApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareasRepository _tareasRepository;
        public TareasController(ITareasRepository tareasRepository)
        {
            _tareasRepository = tareasRepository;
        }

        /// <summary>
        /// Obtiene todas las tareas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTareas()
        {
            var tareas = await _tareasRepository.GetAllTareasAsync();

            return Ok(tareas);
        }

        /// <summary>
        /// Obtiene una tarea en especifico
        /// </summary>
        /// <param name="id">Id de la Tarea a buscar</param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(List<Tarea>), 200)]
        public async Task<IActionResult> GetTarea(int id)
        {
            var tarea = await _tareasRepository.GetTareaById(id);

            if (tarea is null)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(tarea);
        }

        /// <summary>
        /// Añade una nueva tarea
        /// </summary>
        /// <param name="newTarea">Datos de la Tarea a agregar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTarea(AddTarea newTarea)
        {
            var tarea = new Tarea
            {
                Title = newTarea.Title,
                Description = newTarea.Description,
                IsCompleted = newTarea.IsCompleted,
            };

            await _tareasRepository.CreateTarea(tarea);

            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        /// <summary>
        /// Edita una tarea en especifico
        /// </summary>
        /// <param name="id">Id de la Tarea a eliminar</param>
        /// <param name="updatedTarea">Datos de la tarea a actualizar</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditTarea(int id, AddTarea updatedTarea)
        {
            var tarea = await _tareasRepository.UpdateTarea(id, updatedTarea);

            if (tarea is null)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(await GetTarea(id));
        }

        /// <summary>
        /// Elimina una tarea en especifico
        /// </summary>
        /// <param name="id">Id de Tarea a eliminar</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _tareasRepository.DeleteTarea(id);

            if (tarea is null)
                return StatusCode(StatusCodes.Status404NotFound);

            return NoContent();
        }
    }
}