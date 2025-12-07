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
            try
            {
                var inscripcion = await _service.GetByIdAsync(id);
                return Ok(inscripcion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Inscripcion>> CreateInscripcion(CreateInscripcionDTO inscripcion)
        {
            try
            {
                await _service.AddAsync(inscripcion);
                return Ok("Inscripción creada exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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

            try
            {
                await _service.UpdateAsync(existingInscripcionDTO, existingInscripcion.Id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteInscripcion(int id)
       {
           try
           {
               await _service.DeleteAsync(id);
               return NoContent();
           }
           catch (KeyNotFoundException ex)
           {
               return NotFound(ex.Message);
           }
       }

   }
}