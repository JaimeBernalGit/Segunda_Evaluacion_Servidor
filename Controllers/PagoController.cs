using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using CursosAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {

        private readonly IPagoService _pagoService;
        private readonly IAuthService _authService;

        public PagoController(IPagoService pagoService, IAuthService authService)
        {
            _pagoService = pagoService;
            _authService = authService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<List<Pago>>> GetPagos(
            [FromQuery] string? orderBy,
            [FromQuery] bool descending = false)
        {
            var pagos = await _pagoService.GetAllAsync(orderBy, descending);
            return Ok(pagos);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            if (!_authService.HasAccessToResource(usuarioId, User))
                return Forbid();
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Pago>> CreatePago([FromBody] PagoCreateDTO Pago)
        {
            await _pagoService.AddAsync(Pago);
            return Ok(Pago);
        }


    }
}