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

        public CursoController(ICursoService cursoRepository)
        {
            _cursoService = cursoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos(
            [FromQuery] string? titulo,
            [FromQuery] DateTime? fecha)

        {
            //Implementar metodo GetAllFilteredAsync()
            var Cursos = await _cursoService.GetAllAsync();
            return Ok(Cursos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCursoById(int id)
        {
            var Curso = await _cursoService.GetByIdAsync(id);
            if (Curso == null)
            {
                return NotFound();
            }
            return Ok(Curso);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> CreateCurso(CursoCreateDTO Curso)
        {
            await _cursoService.AddAsync(Curso);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, [FromBody] CursoUpdateDTO updatedCurso)
        {
            
            if(id<=0){
                return BadRequest();
            }
            await _cursoService.UpdateAsync(id, updatedCurso);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            try
            {
                await _cursoService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


    }
}