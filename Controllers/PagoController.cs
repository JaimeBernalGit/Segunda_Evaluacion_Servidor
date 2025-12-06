using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using CursosAPI.Repositories;
namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PagoController : ControllerBase
   {

    private readonly IPagoRepository _pagoRepository;

    public PagoController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> GetPagos()
        {
            var Pagos = await _pagoRepository.GetAllAsync();
            return Ok(Pagos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var Pago = await _pagoRepository.GetByIdAsync(id);
            if (Pago == null)
            {
                return NotFound();
            }
            return Ok(Pago);
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago(PagoCreateDTO Pago)
        {
            await _pagoRepository.AddAsync(Pago);
            return Ok(Pago);
        }


   }
}