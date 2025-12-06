using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LeccionController : ControllerBase
   {
    private static List<Leccion> leccions = new List<Leccion>();

    private readonly ILeccionService _service;

    public LeccionController(ILeccionService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Leccion>>> GetLeccions(
            [FromQuery] string? titulo,
            [FromQuery] int? duracionMinima,
            [FromQuery] int? duracionMaxima
        )
        {
            var leccions = await _service.GetAllAsync(titulo, duracionMinima, duracionMaxima);
            return Ok(leccions);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Leccion>> GetLeccion(int id)
        {
            var leccion = await _service.GetByIdAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }
            return Ok(leccion);
        }

        [HttpPost]
        public async Task<ActionResult<Leccion>> CreateLeccion(CreateLeccionDTO leccion)
        {
            await _service.AddAsync(leccion);
            return Ok(leccion);
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeccion(int id, UpdateLeccionDTO updatedLeccion)
        {
            var existingLeccion = await _service.GetByIdAsync(id);
            if (existingLeccion == null)
            {
                return NotFound();
            }
            var existingLeccionDTO = new UpdateLeccionDTO();
            existingLeccionDTO.Titulo = updatedLeccion.Titulo;
            existingLeccionDTO.ContenidoUrl = updatedLeccion.ContenidoUrl;
            existingLeccionDTO.Descripción = updatedLeccion.Descripción;
            existingLeccionDTO.DuracionMin = updatedLeccion.DuracionMin;

            await _service.UpdateAsync(existingLeccionDTO, existingLeccion.Id);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteLeccion(int id)
       {
           var leccion = await _service.GetByIdAsync(id);
           if (leccion == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}