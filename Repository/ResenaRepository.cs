using Models;
using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class ResenaRepository : IResenaRepository
    {
        private readonly string _connectionString;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICursoRepository _cursoRepository;


        public ResenaRepository(IConfiguration configuration, IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
            _usuarioRepository = usuarioRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Resena>> GetAllAsync()
        {
            var Resenas = new List<Resena>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT resena_id, usuario_id, curso_id, calificacion, comentario, fecha_publicacion FROM Resena";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Resena = new Resena
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                Calificacion = reader.GetInt32(3),
                                Comentario = reader.GetString(4),
                                FechaPublicacion = reader.GetDateTime(5)


                            };

                            Resenas.Add(Resena);
                        }
                    }
                }
            }
            return Resenas;
        }

        public async Task<Resena> GetByIdAsync(int id)
        {
            Resena Resena = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT resena_id, usuario_id, curso_id, calificacion, comentario, fecha_publicacion FROM Resena WHERE resena_id = @resena_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@resena_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Resena = new Resena
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                Calificacion = reader.GetInt32(3),
                                Comentario = reader.GetString(4),
                                FechaPublicacion = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return Resena;
        }

        public async Task AddAsync(ResenaCreateDTO Resena)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Resena (usuario_id, curso_id, calificacion, comentario) VALUES (@usuario_id, @curso_id, @calificacion, @comentario);";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", Resena.UsuarioId);
                    command.Parameters.AddWithValue("@curso_id", Resena.CursoId);
                    command.Parameters.AddWithValue("@calificacion", Resena.Calificacion);
                    command.Parameters.AddWithValue("@comentario", Resena.Comentario);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        //Solo se permite actualizar calificacion y comentario
        //No se debe poder modificar quien ha hecho el comentario , ni sobre que curso lo ha hecho
        public async Task UpdateAsync(Resena Resena)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Resena SET  calificacion = @calificacion, comentario = @comentario WHERE resena_id = @resena_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@resena_id", Resena.Id);
                    command.Parameters.AddWithValue("@calificacion", Resena.Calificacion);
                    command.Parameters.AddWithValue("@comentario", Resena.Comentario);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Resena WHERE resena_id = @resena_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@resena_id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }




    }

}