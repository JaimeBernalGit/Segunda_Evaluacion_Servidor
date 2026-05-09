using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
namespace CursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        private readonly IUsuarioService _service;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
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
        public async Task<ActionResult<UserDtoOut>> GetUsuario(int id)
        {
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
        public async Task<IActionResult> UpdateUsuario(int id, UpdateUsuarioDTO updatedUsuario, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {
            var adminApiKey = _configuration["AdminSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida.");
            }
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
        public async Task<IActionResult> DeleteUsuario(int id, [FromHeader(Name = "X-Admin-Key")] string? apiKey)
        {

            var adminApiKey = _configuration["AdminSettings:ApiKey"];
            if (string.IsNullOrEmpty(apiKey) || apiKey != adminApiKey)
            {
                return Unauthorized("Acceso denegado: ApiKey inválida.");
            }
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