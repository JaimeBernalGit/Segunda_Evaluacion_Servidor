using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeccionController : ControllerBase
    {
        private readonly ILeccionService _service;
        private readonly IConfiguration _configuration;

        public LeccionController(ILeccionService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
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
        public async Task<ActionResult<Leccion>> CreateLeccion(CreateLeccionDTO leccion, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {
            var adminApiKey = _configuration["AdminSettings:ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida o faltante.");
            }

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
        public async Task<IActionResult> UpdateLeccion(int id, UpdateLeccionDTO updatedLeccion, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {
            var adminApiKey = _configuration["AdminSettings:ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida o faltante.");
            }

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
        public async Task<IActionResult> DeleteLeccion(int id, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {
            var adminApiKey = _configuration["AdminSettings:ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida o faltante.");
            }
            
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