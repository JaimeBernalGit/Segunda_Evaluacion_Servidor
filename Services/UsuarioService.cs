using CursosAPI.Repositories;
using Models;
namespace CursosAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUploadDocService _uploadDocService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUploadDocService uploadDocService)
        {
            _usuarioRepository = usuarioRepository;
            _uploadDocService = uploadDocService;
        }

        public async Task<List<UserDtoOut>> GetAllAsync(string? nombre = null, string? estado = null, DateTime? fechaRegistroDesde = null, DateTime? fechaRegistroHasta = null, string? orderBy = null, bool descending = false)
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
                usuarios = usuarios.Where(u => u.Fecha_Registro.Date >= fechaRegistroDesde.Value.Date).ToList();

            if (fechaRegistroHasta.HasValue)
                usuarios = usuarios.Where(u => u.Fecha_Registro.Date <= fechaRegistroHasta.Value.Date).ToList();

            usuarios = orderBy?.ToLower() switch
            {
                "fecha" => descending ? usuarios.OrderByDescending(u => u.Fecha_Registro).ToList()
                                    : usuarios.OrderBy(u => u.Fecha_Registro).ToList(),
                _       => usuarios
            };

            return usuarios;
        }

        public async Task<UserDtoOut?> GetByIdAsync(int id)
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

            var fotoPerfilUrl = await _uploadDocService.UploadImageAsync(usuario.FotoPerfil);

            var usuarioIn = new UserInDto
            {
                FotoPerfilUrl = fotoPerfilUrl,
                Nombre = usuario.Nombre,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Password = usuario.Password,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
                
            await _usuarioRepository.AddAsync(usuarioIn);
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

            var fotoPerfilUrl = await _uploadDocService.UploadImageAsync(usuario.FotoPerfil);

            var usuarioIn = new UpdateUsuarioInDTO
            {
                FotoPerfilUrl = fotoPerfilUrl,
                Nombre = usuario.Nombre,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Password = usuario.Password,
                Correo = usuario.Correo,
                Estado = usuario.Estado
            };

            await _usuarioRepository.UpdateAsync(usuarioIn, id);
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
