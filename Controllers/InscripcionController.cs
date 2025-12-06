using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
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
        public async Task<ActionResult<List<Inscripcion>>> GetInscripciones(
            [FromQuery] string? estado,
            [FromQuery] int? progresoMinimo,
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta
        )
        {
            var inscripciones = await _service.GetAllAsync(estado, progresoMinimo, fechaDesde, fechaHasta);
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
        public async Task<ActionResult<Inscripcion>> CreateInscripcion(CreateInscripcionDTO inscripcion)
        {
            await _service.AddAsync(inscripcion);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInscripcion(int id, UpdateInscripcionDTO updatedInscripcion)
        {
            var existingInscripcion = await _service.GetByIdAsync(id);
            if (existingInscripcion == null)
            {
                return NotFound();
            }
            var existingInscripcionDTO = new UpdateInscripcionDTO();
            existingInscripcionDTO.ProgresoPorcentaje = updatedInscripcion.ProgresoPorcentaje;
            existingInscripcionDTO.Estado = updatedInscripcion.Estado;

            await _service.UpdateAsync(existingInscripcionDTO, existingInscripcion.Id);
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