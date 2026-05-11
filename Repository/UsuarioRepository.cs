using Microsoft.Data.SqlClient;
using Models;

namespace CursosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
        }

        public async Task<List<UserDtoOut>> GetAllAsync()
        {
          var usuarios = new List<UserDtoOut>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT usuario_id, nombre, nombre_usuario, email, fecha_registro, estado, rol FROM Usuario";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new UserDtoOut
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Nombre_Usuario = reader.GetString(2),
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4),
                                Estado = reader.GetString(5),
                                Rol = reader.GetString(6)
                            }; 

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<UserDtoOut> GetByIdAsync(int id)
        {
            UserDtoOut usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT usuario_id, nombre, nombre_usuario, email, fecha_registro, estado, rol FROM Usuario WHERE usuario_id = @usuario_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new UserDtoOut
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Nombre_Usuario = reader.GetString(2),
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4),
                                Estado = reader.GetString(5),
                                Rol = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(CreateUsuarioDTO usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (nombre, nombre_usuario, password, email, fecha_registro, estado, rol) VALUES (@nombre, @nombre_usuario, @password, @email, @fecha_registro, @estado, @rol)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre_Usuario);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@email", usuario.Correo);
                    command.Parameters.AddWithValue("@fecha_registro", DateTime.Now);
                    command.Parameters.AddWithValue("@estado", "Inactivo");
                    command.Parameters.AddWithValue("@rol", usuario.Rol);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(UpdateUsuarioDTO usuario, int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET nombre = @nombre, nombre_usuario = @nombre_usuario, password = @password, email = @email, estado = @estado WHERE usuario_id = @usuario_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre_Usuario);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@email", usuario.Correo);
                    command.Parameters.AddWithValue("@estado", usuario.Estado);
                    command.Parameters.AddWithValue("@usuario_id", id);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario WHERE usuario_id = @usuario_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<UserDtoOut> GetUserFromCredentials(LoginDTO loginDTO)
        {
            UserDtoOut usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT usuario_id, nombre, nombre_usuario, email, fecha_registro, estado, rol FROM Usuario WHERE nombre_usuario = @nombre_usuario AND password = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre_usuario", loginDTO.Nombre_Usuario);
                    command.Parameters.AddWithValue("@password", loginDTO.Password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new UserDtoOut
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Nombre_Usuario = reader.GetString(2),
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4),
                                Estado = reader.GetString(5),
                                Rol = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task<UserDtoOut> AddUserFromCredentials(RegisterDTO registerDTO) {
            UserDtoOut usuario = null;

           
            return usuario;
        }
        

    }

}