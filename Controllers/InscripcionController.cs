using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class InscripcionController : ControllerBase
   {
    private static List<Inscripcion> inscripciones = new List<Inscripcion>();

    private readonly IInscripcionService _service;

    public InscripcionController(IInscripcionService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Inscripcion>>> GetInscripciones()
        {
            var inscripciones = await _service.GetAllAsync();
            return Ok(inscripciones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
        {
            var inscripcion = await _service.GetByIdAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            return Ok(inscripcion);
        }

        [HttpPost]
        public async Task<ActionResult<Inscripcion>> CreateInscripcion(Inscripcion inscripcion)
        {
            await _service.AddAsync(inscripcion);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInscripcion(int id, Inscripcion updatedInscripcion)
        {
            var existingInscripcion = await _service.GetByIdAsync(id);
            if (existingInscripcion == null)
            {
                return NotFound();
            }

            existingInscripcion.Curso = updatedInscripcion.Curso;
            existingInscripcion.Id = updatedInscripcion.Id;
            existingInscripcion.Estado = updatedInscripcion.Estado;
            existingInscripcion.FechaInscripcion = updatedInscripcion.FechaInscripcion;
            existingInscripcion.ProgresoPorcentaje = updatedInscripcion.ProgresoPorcentaje;
            existingInscripcion.Usuario = updatedInscripcion.Usuario;

            await _service.UpdateAsync(existingInscripcion);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteInscripcion(int id)
       {
           var inscripcion = await _service.GetByIdAsync(id);
           if (inscripcion == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}