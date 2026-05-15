using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using Microsoft.AspNetCore.Authorization;
namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        private readonly IUsuarioService _service;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public UsuarioController(IUsuarioService service, IConfiguration configuration, IAuthService authService)
        {
            _service = service;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<List<UserDtoOut>>> GetUsuarios(
            [FromQuery] string? nombre,
            [FromQuery] string? estado,
            [FromQuery] DateTime? fechaRegistroDesde,
            [FromQuery] DateTime? fechaRegistroHasta
        )
        {
            var usuarios = await _service.GetAllAsync(nombre, estado, fechaRegistroDesde, fechaRegistroHasta);
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDtoOut>> GetUsuario(int id)
        {
            if (!_authService.HasAccessToResource(id, User))
                return Forbid();
            try
            {
                var usuario = await _service.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<Usuario>> CreateUsuario(CreateUsuarioDTO usuario)
        {
            try
            {
                await _service.AddAsync(usuario);
                return Ok("Usuario creado exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUsuario(int id, UpdateUsuarioDTO updatedUsuario)
        {
            if (!_authService.HasAccessToResource(id, User))
                return Forbid();
            
            var existingUsuario = await _service.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }
            var existingUsuarioDTO = new UpdateUsuarioDTO();
            existingUsuarioDTO.Correo = updatedUsuario.Correo;
            existingUsuarioDTO.Estado = updatedUsuario.Estado;
            existingUsuarioDTO.Nombre_Usuario = updatedUsuario.Nombre_Usuario;
            existingUsuarioDTO.Nombre = updatedUsuario.Nombre;
            existingUsuarioDTO.Password = updatedUsuario.Password;

            try
            {
                await _service.UpdateAsync(existingUsuarioDTO, existingUsuario.Id);
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
        public async Task<IActionResult> DeleteUsuario(int id)
        {

           
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            try
            {
               await _service.DeleteAsync(id);
               return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
               return NotFound(ex.Message);
            }
        }

    }
}