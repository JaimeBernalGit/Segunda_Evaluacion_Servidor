using Microsoft.AspNetCore.Mvc;
using CursosAPI.Repositories;
using Models;
// using CursosAPI.Services;

namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private static List<Curso> Cursos = new List<Curso>();

        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos()
        {
            var Cursos = await _cursoRepository.GetAllAsync();
            return Ok(Cursos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var Curso = await _cursoRepository.GetByIdAsync(id);
            if (Curso == null)
            {
                return NotFound();
            }
            return Ok(Curso);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> CreateCurso(CursoCreateDTO Curso)
        {
            var newId = await _cursoRepository.AddAsync(Curso);

            var CursoCreado = new Curso
            {
                Id = newId,
                Titulo = Curso.Titulo,
                Descripcion = Curso.Descripcion,
                Categoria = Curso.Categoria,
                Nivel = Curso.Nivel,
                Precio = Curso.Precio,
                Fecha_Creacion = DateTime.Now
            };


            return CreatedAtAction(nameof(GetCurso), new { id = newId }, CursoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, Curso updatedCurso)
        {
            var existingCurso = await _cursoRepository.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound();
            }

            existingCurso.Titulo = updatedCurso.Titulo;
            existingCurso.Descripcion = updatedCurso.Descripcion;
            existingCurso.Categoria = updatedCurso.Categoria;
            existingCurso.Nivel = updatedCurso.Nivel;
            existingCurso.Precio = updatedCurso.Precio;

            await _cursoRepository.UpdateAsync(existingCurso);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var Curso = await _cursoRepository.GetByIdAsync(id);
            if (Curso == null)
            {
                return NotFound();
            }
            await _cursoRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}