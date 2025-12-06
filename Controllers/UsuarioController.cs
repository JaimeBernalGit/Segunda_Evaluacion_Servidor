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
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuarios = await _service.GetAllAsync();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _service.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(CreateUsuarioDTO usuario)
        {
            await _service.AddAsync(usuario);
            return Ok(usuario);
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

            await _service.UpdateAsync(existingUsuarioDTO, existingUsuario.Id);
            return NoContent();
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
            await _service.DeleteAsync(id);
            return NoContent();
        }

    }
}