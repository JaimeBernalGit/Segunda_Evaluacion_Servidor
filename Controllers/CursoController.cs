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
        public async Task<ActionResult<List<Curso>>> GetCursos()
        {
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
        public async Task<IActionResult> UpdateCurso(int id, Curso updatedCurso)
        {
            var existingCurso = await _cursoService.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound();
            }

            existingCurso.Titulo = updatedCurso.Titulo;
            existingCurso.Descripcion = updatedCurso.Descripcion;
            existingCurso.Categoria = updatedCurso.Categoria;
            existingCurso.Nivel = updatedCurso.Nivel;
            existingCurso.Precio = updatedCurso.Precio;

            await _cursoService.UpdateAsync(existingCurso);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var Curso = await _cursoService.GetByIdAsync(id);
            if (Curso == null)
            {
                return NotFound();
            }
            await _cursoService.DeleteAsync(id);
            return NoContent();
        }


    }
}