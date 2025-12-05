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

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
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
