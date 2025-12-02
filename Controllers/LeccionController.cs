using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class LeccionController : ControllerBase
   {
    private static List<Leccion> lecciones = new List<Leccion>();

    private readonly ILeccionService _service;

    public LeccionController(ILeccionService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Leccion>>> GetLecciones()
        {
            var lecciones = await _service.GetAllAsync();
            return Ok(lecciones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Leccion>> GetLeccion(int id)
        {
            var leccion = await _service.GetByIdAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }
            return Ok(leccion);
        }

        [HttpPost]
        public async Task<ActionResult<Leccion>> CreateLeccion(Leccion leccion)
        {
   
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeccion(Leccion leccion)
        {

            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteLeccion(int id)
       {
           var leccion = await _service.GetByIdAsync(id);
           if (leccion == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}