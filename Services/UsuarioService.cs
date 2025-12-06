using CursosAPI.Repositories;
using Models;
namespace CursosAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            
        }

        public async Task<List<Usuario>> GetAllAsync(string? nombre = null, string? estado = null, DateTime? fechaRegistroDesde = null, DateTime? fechaRegistroHasta = null)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(nombre))
            {
                nombre = nombre.Trim();
                usuarios = usuarios
                    .Where(u => u.Nombre.Contains(nombre) || 
                                u.Nombre_Usuario.Contains(nombre))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(estado))
            {
                estado = estado.Trim();
                usuarios = usuarios
                    .Where(u => u.Estado.Equals(estado))
                    .ToList();
            }

            if (fechaRegistroDesde.HasValue)
            {
                usuarios = usuarios
                    .Where(u => u.Fecha_Registro.Date >= fechaRegistroDesde.Value.Date)
                    .ToList();
            }

            if (fechaRegistroHasta.HasValue)
            {
                usuarios = usuarios
                    .Where(u => u.Fecha_Registro.Date <= fechaRegistroHasta.Value.Date)
                    .ToList();
            }
            
            return usuarios;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateUsuarioDTO usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(UpdateUsuarioDTO usuario, int id)
        {
            await _usuarioRepository.UpdateAsync(usuario, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
