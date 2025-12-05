using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
          var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT usuario_id, nombre, nombre_usuario, password, email, fecha_registro, estado FROM Usuario";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Nombre_Usuario = reader.GetString(2),
                                Password = reader.GetString(3),
                                Correo = reader.GetString(4),
                                Fecha_Registro = reader.GetDateTime(5),
                                Estado = reader.GetString(6)
                            }; 

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            Usuario usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT usuario_id, nombre, nombre_usuario, password, email, fecha_registro, estado FROM Usuario WHERE usuario_id = @usuario_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Nombre_Usuario = reader.GetString(2),
                                Password = reader.GetString(3),
                                Correo = reader.GetString(4),
                                Fecha_Registro = reader.GetDateTime(5),
                                Estado = reader.GetString(6)
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

                string query = "INSERT INTO Usuario (nombre, nombre_usuario, password, email, fecha_registro, estado) VALUES (@nombre, @nombre_usuario, @password, @email, @fecha_registro, @estado)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@nombre_usuario", usuario.Nombre_Usuario);
                    command.Parameters.AddWithValue("@password", usuario.Password);
                    command.Parameters.AddWithValue("@email", usuario.Correo);
                    command.Parameters.AddWithValue("@fecha_registro", DateTime.Now);
                    command.Parameters.AddWithValue("@estado", "Inactivo");

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


    }

}