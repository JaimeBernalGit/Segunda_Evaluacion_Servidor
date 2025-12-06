using Microsoft.AspNetCore.Mvc;
using CursosAPI.Services;
using Models;
using CursosAPI.Repositories;


namespace ResenasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenaController : ControllerBase
    {
        private readonly IResenaRepository _resenaRepository;

        public ResenaController(IResenaRepository resenaRepository)
        {
            _resenaRepository = resenaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Resena>>> GetResenas(
            [FromQuery] int? calificacion,
            [FromQuery] DateTime? fecha
            )

        {

            var Resenas = await _resenaRepository.GetAllAsync();
            return Ok(Resenas);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resena>> GetResenaById(int id)
        {
            try
            {
                var Resena = await _resenaRepository.GetByIdAsync(id);
                return Ok(Resena);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Resena>> CreateResena([FromBody] ResenaCreateDTO Resena)
        {
            try
            {
                await _resenaRepository.AddAsync(Resena);
                return Ok("Resena creado exitosamente");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResena(int id, [FromBody] Resena updatedResena)
        {

            try
            {
                var existingResena = await _resenaRepository.GetByIdAsync(id);
                if (id <= 0) return BadRequest("El ID no es válido");
                existingResena.Calificacion = updatedResena.Calificacion;
                existingResena.Comentario = updatedResena.Comentario;
                await _resenaRepository.UpdateAsync(existingResena);
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
        public async Task<IActionResult> DeleteResena(int id)
        {
            try
            {
                await _resenaRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
        }


    }
}