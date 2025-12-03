using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ReseñaController : ControllerBase
   {
    private static List<Reseña> reseñas = new List<Reseña>();

    private readonly IReseñaService _service;

    public ReseñaController(IReseñaService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Reseña>>> GetReseñas()
        {
            var reseñas = await _service.GetAllAsync();
            return Ok(reseñas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Reseña>> GetReseña(int id)
        {
            var reseña = await _service.GetByIdAsync(id);
            if (reseña == null)
            {
                return NotFound();
            }
            return Ok(reseña);
        }

        [HttpPost]
        public async Task<ActionResult<Reseña>> CreateReseña(Reseña reseña)
        {
            await _service.AddAsync(reseña);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReseña(int id, Reseña updatedReseña)
        {
            var existingReseña = await _service.GetByIdAsync(id);
            if (existingReseña == null)
            {
                return NotFound();
            }

            existingReseña.Curso = updatedReseña.Curso;
            existingReseña.Id = updatedReseña.Id;
            existingReseña.Calificacion = updatedReseña.Calificacion;
            existingReseña.Comentario = updatedReseña.Comentario;
            existingReseña.FechaPublicacion = updatedReseña.FechaPublicacion;
            existingReseña.Usuario = updatedReseña.Usuario;

            await _service.UpdateAsync(existingReseña);
            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteReseña(int id)
       {
           var reseña = await _service.GetByIdAsync(id);
           if (reseña == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}