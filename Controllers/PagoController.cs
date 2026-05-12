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

        private readonly IPagoService _pagoService;
        private readonly IAuthService _authservice;

        public PagoController(IPagoService pagoService, IAuthService authService)
        {
            _pagoService = pagoService;
            _authservice = authService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pago>>> GetPagos()
        {
            var Pagos = await _pagoService.GetAllAsync();
            return Ok(Pagos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var Pago = await _pagoService.GetByIdAsync(id);
            if (Pago == null)
            {
                return NotFound();
            }
            return Ok(Pago);
        }

        // consulta todos los pagos que ha hecho un usuario
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            try
            {
                var resenas = await _pagoService.GetByUsuarioAsync(usuarioId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // consulta las personas que han pagado por un curso
        [HttpGet("curso/{cursoId}")]
        public async Task<IActionResult> GetByCurso(int cursoId)
        {
            try
            {
                var resenas = await _pagoService.GetByCursoAsync(cursoId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago([FromBody] PagoCreateDTO Pago)
        {
            await _pagoService.AddAsync(Pago);
            return Ok(Pago);
        }


    }
}