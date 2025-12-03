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
            await _service.AddAsync(pago);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(int id, Pago updatedPago)
        {
            var existingPago = await _service.GetByIdAsync(id);
            if (existingPago == null)
            {
                return NotFound();
            }

            existingPago.Curso = updatedPago.Curso;
            existingPago.Id = updatedPago.Id;
            existingPago.Cantidad = updatedPago.Cantidad;
            existingPago.Holder_Name = updatedPago.Holder_Name;
            existingPago.Holder_Number = updatedPago.Holder_Number;
            existingPago.Usuario = updatedPago.Usuario;
            existingPago.CVV = updatedPago.CVV;

            await _service.UpdateAsync(existingPago);
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