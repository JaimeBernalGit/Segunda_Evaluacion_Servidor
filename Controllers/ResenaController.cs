using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using CursosAPI.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace ResenasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenaController : ControllerBase
    {
        private readonly IResenaService _resenaService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public ResenaController(IResenaService resenaService, IConfiguration configuration, IAuthService authService)
        {
            _resenaService = resenaService;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Resena>>> GetResenas(
            [FromQuery] int? calificacion,
            [FromQuery] DateTime? fecha
            )

        {

            var Resenas = await _resenaService.GetAllAsync(calificacion, fecha);
            return Ok(Resenas);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Resena>> GetResenaById(int id)
        {
            try
            {
                var Resena = await _resenaService.GetByIdAsync(id);
                return Ok(Resena);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Nos trae las reseñas de un usuario por curso
        [HttpGet("usuario/{usuarioId}/curso/{cursoId}")]
        public async Task<IActionResult> GetByUsuarioYCurso(int usuarioId, int cursoId)
        {
            try
            {
                var resenas = await _resenaService.GetByUsuarioAndCursoAsync(usuarioId, cursoId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Consulta las reseñas de un usuario
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            try
            {
                var resenas = await _resenaService.GetByUsuarioAsync(usuarioId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        //Consulta las reseñas por curso
        [HttpGet("curso/{cursoId}")]
        public async Task<IActionResult> GetByCurso(int cursoId)
        {
            try
            {
                var resenas = await _resenaService.GetByCursoAsync(cursoId);
                return Ok(resenas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Resena>> CreateResena([FromBody] ResenaCreateDTO Resena)
        {
            try
            {
                await _resenaService.AddAsync(Resena);
                return Ok("Resena creado exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateResena(int id, [FromBody] ResenaUpdateDTO updatedResena)
        {
            if (!_authService.HasAccessToOwnResource(id, User))
                return Forbid();

            try
            {
                await _resenaService.UpdateAsync(id, updatedResena);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteResena(int id)
        {
            
            try
            {
                await _resenaService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }


    }
}