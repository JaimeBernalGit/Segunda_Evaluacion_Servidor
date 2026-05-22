using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using Microsoft.AspNetCore.Authorization;
namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class InscripcionController : ControllerBase
   {
    private static List<Inscripcion> inscripciones = new List<Inscripcion>();

    private readonly IInscripcionService _service;
    private readonly IAuthService _authService;

    public InscripcionController(IInscripcionService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }
    
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<List<Inscripcion>>> GetInscripciones(
            [FromQuery] string? estado,
            [FromQuery] int? progresoMinimo,
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] string? orderBy,
            [FromQuery] bool descending = false)
        {
            var inscripciones = await _service.GetAllAsync(estado, progresoMinimo, fechaDesde, fechaHasta, orderBy, descending);
            return Ok(inscripciones);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
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
        [HttpGet("usuario/{usuarioId}")]
        [Authorize]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            if (!_authService.HasAccessToResource(usuarioId, User))
                return Forbid();
            try
            {
                var resenas = await _service.GetByUsuarioAsync(usuarioId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
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
       [Authorize(Roles = Roles.Admin)]
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
       [Authorize(Roles = Roles.Admin)]
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