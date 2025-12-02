using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PagoController : ControllerBase
   {
    private static List<Pago> pagos = new List<Pago>();

    private readonly IPagoService _service;

    public PagoController(IPagoService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> GetPagos()
        {
            var pagos = await _service.GetAllAsync();
            return Ok(pagos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _service.GetByIdAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago(Pago pago)
        {
   
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(Pago pago)
        {

            return NoContent();
        }
  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePago(int id)
       {
           var pago = await _service.GetByIdAsync(id);
           if (pago == null)
           {
               return NotFound();
           }
           await _service.DeleteAsync(id);
           return NoContent();
       }

   }
}