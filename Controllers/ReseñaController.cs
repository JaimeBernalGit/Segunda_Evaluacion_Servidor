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
   
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReseña(Reseña reseña)
        {

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