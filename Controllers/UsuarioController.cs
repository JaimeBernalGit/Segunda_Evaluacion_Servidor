using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;

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
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            await _service.AddAsync(usuario);
            return Ok();
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuario updatedUsuario)
        {
            var existingUsuario = await _service.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            existingUsuario.Correo = updatedUsuario.Correo;
            existingUsuario.Id = updatedUsuario.Id;
            existingUsuario.Estado = updatedUsuario.Estado;
            existingUsuario.Direccion = updatedUsuario.Direccion;
            existingUsuario.Nombre = updatedUsuario.Nombre;
            existingUsuario.Fecha_Registro = updatedUsuario.Fecha_Registro;

            await _service.UpdateAsync(existingUsuario);
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