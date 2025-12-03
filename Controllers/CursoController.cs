using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CursoController : ControllerBase
   {
    private static List<Curso> cursos = new List<Curso>();

    private readonly ICursoService _service;

    public CursoController(ICursoService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetCursos()
        {
            var cursos = await _service.GetAllAsync();
            return Ok(cursos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _service.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> CreateCurso(Curso curso)
        {
            await _service.AddAsync(curso);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, Curso updatedCurso)
        {
            var existingCurso = await _service.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound();
            }

            existingCurso.Categoria = updatedCurso.Categoria;
            existingCurso.Id = updatedCurso.Id;
            existingCurso.Descripcion = updatedCurso.Descripcion;
            existingCurso.Fecha_Creacion = updatedCurso.Fecha_Creacion;
            existingCurso.Nivel = updatedCurso.Nivel;
            existingCurso.Titulo = updatedCurso.Titulo;

            await _service.UpdateAsync(existingCurso);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCurso(int id)
       {
           var curso = await _service.GetByIdAsync(id);
           if (curso == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}