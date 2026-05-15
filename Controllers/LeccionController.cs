using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using Microsoft.AspNetCore.Authorization;
namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeccionController : ControllerBase
    {
        private readonly ILeccionService _service;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public LeccionController(ILeccionService service, IConfiguration configuration, IAuthService authService)
        {
            _service = service;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Leccion>> GetLeccion(int id)
        {
            try
            {
                var leccion = await _service.GetByIdAsync(id);
                return Ok(leccion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Leccion>> CreateLeccion(CreateLeccionDTO leccion)
        {

            try
            {
                await _service.AddAsync(leccion);
                return Ok("Lección creada exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
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
            try
            {
                await _service.UpdateAsync(existingLeccionDTO, existingLeccion.Id);
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
        public async Task<IActionResult> DeleteLeccion(int id)
        {
            
            
            var leccion = await _service.GetByIdAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }
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