using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;


namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly IConfiguration _configuration;

        public CursoController(ICursoService cursoService, IConfiguration configuration)
        {
            _cursoService = cursoService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos(
            [FromQuery] string? titulo,
            [FromQuery] string? categoria
            )

        {

            var cursos = await _cursoService.GetAllAsync(titulo, categoria);
            return Ok(cursos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCursoById(int id)
        {
            try
            {
                var curso = await _cursoService.GetByIdAsync(id);
                return Ok(curso);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> CreateCurso([FromBody] CursoCreateDTO Curso)
        {
            try
            {
                await _cursoService.AddAsync(Curso);
                return Ok("Curso creado exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, [FromBody] CursoUpdateDTO updatedCurso)
        {

            if (id <= 0) return BadRequest("El ID no es válido");

            try
            {
                await _cursoService.UpdateAsync(id, updatedCurso);
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
        public async Task<IActionResult> DeleteCurso(int id, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {
            var adminApiKey = _configuration["AdminSettings:ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida o faltante.");
            }
            try
            {
                await _cursoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }


    }
}