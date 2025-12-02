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
   
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(Curso curso)
        {

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