using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
namespace CursosAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsuarioController : ControllerBase
   {
    private static List<Usuario> usuarios = new List<Usuario>();

    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios(
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
        public async Task<IActionResult> UpdateUsuario(int id, UpdateUsuarioDTO updatedUsuario)
        {
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
       public async Task<IActionResult> DeleteUsuario(int id)
       {
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