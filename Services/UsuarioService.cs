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
                usuarios = usuarios.Where(u => u.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase) || u.Nombre_Usuario.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(estado))
            {
                estado = estado.Trim();
                usuarios = usuarios.Where(u => u.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (fechaRegistroDesde.HasValue)
            {
                usuarios = usuarios.Where(u => u.Fecha_Registro.Date >= fechaRegistroDesde.Value.Date).ToList();
            }

            if (fechaRegistroHasta.HasValue)
            {
                usuarios = usuarios.Where(u => u.Fecha_Registro.Date <= fechaRegistroHasta.Value.Date).ToList();
            }
            
            return usuarios;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null || usuario.Id < 0)
            {
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
            }
            return usuario;
        }

        public async Task AddAsync(CreateUsuarioDTO usuario)
        {
            var usuariosOnline = await _usuarioRepository.GetAllAsync();

            foreach (var u in usuariosOnline)
            {
                if (u.Nombre_Usuario.Equals(usuario.Nombre_Usuario, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"El nombre de usuario '{usuario.Nombre_Usuario}' ya está en uso.");
                }
            }

            foreach (var u in usuariosOnline)
            {
                if (u.Correo.Equals(usuario.Correo, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException($"El correo '{usuario.Correo}' ya está registrado.");
                }
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new InvalidOperationException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre_Usuario))
            {
                throw new InvalidOperationException("El nombre de usuario no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Password))
            {
                throw new InvalidOperationException("La contraseña no puede estar vacía.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                throw new InvalidOperationException("El correo no puede estar vacío.");
            }

            if (!usuario.Correo.Contains("@"))
            {
                throw new InvalidOperationException("El correo debe tener un formato válido.");
            }

            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateAsync(UpdateUsuarioDTO usuario, int id)
        {
            var usuarioExistente = await GetByIdAsync(id);
            
            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            var usuariosOnline = await _usuarioRepository.GetAllAsync();

            foreach (var u in usuariosOnline)
            {
                if (u.Nombre_Usuario.Equals(usuario.Nombre_Usuario, StringComparison.OrdinalIgnoreCase) && u.Id != id)
                {
                    throw new InvalidOperationException($"El nombre de usuario '{usuario.Nombre_Usuario}' ya está en uso.");
                }
            }

            foreach (var u in usuariosOnline)
            {
                if (u.Correo.Equals(usuario.Correo, StringComparison.OrdinalIgnoreCase) && u.Id != id)
                {
                    throw new InvalidOperationException($"El correo '{usuario.Correo}' ya está registrado.");
                }
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new InvalidOperationException("El nombre no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre_Usuario))
            {
                throw new InvalidOperationException("El nombre de usuario no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Password))
            {
                throw new InvalidOperationException("La contraseña no puede estar vacía.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Correo))
            {
                throw new InvalidOperationException("El correo no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Estado))
            {
                throw new InvalidOperationException("El estado no puede estar vacío.");
            }

            await _usuarioRepository.UpdateAsync(usuario, id);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            await _usuarioRepository.DeleteAsync(id);
        }
    }
}
