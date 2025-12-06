using Microsoft.Data.SqlClient;
using Models;

namespace CursosAPI.Repositories
{
    public class LeccionRepository : ILeccionRepository
    {
        private readonly string _connectionString;
        private readonly ICursoRepository _cursoRepository;

        public LeccionRepository(IConfiguration configuration, ICursoRepository cursoRepository)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Leccion>> GetAllAsync()
        {
          var leccions = new List<Leccion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT leccion_id, curso_id, titulo, contenido_url, duracion_min, descripcion FROM Leccion";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var leccion = new Leccion
                            {
                                Id = reader.GetInt32(0),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(1)),
                                Titulo = reader.GetString(2),
                                ContenidoUrl = reader.GetString(3),
                                DuracionMin = reader.GetInt32(4),
                                Descripción = reader.GetString(5)
                            }; 

                            leccions.Add(leccion);
                        }
                    }
                }
            }
            return leccions;
        }

        public async Task<Leccion> GetByIdAsync(int id)
        {
            Leccion leccion = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT leccion_id, curso_id, titulo, contenido_url, duracion_min, descripcion FROM Leccion WHERE leccion_id = @leccion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@leccion_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            leccion = new Leccion
                            {
                                Id = reader.GetInt32(0),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(1)),
                                Titulo = reader.GetString(2),
                                ContenidoUrl = reader.GetString(3),
                                DuracionMin = reader.GetInt32(4),
                                Descripción = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return leccion;
        }

        public async Task AddAsync(CreateLeccionDTO leccion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Leccion (curso_id, titulo, contenido_url, duracion_min, descripcion) VALUES (@curso_id, @titulo, @contenido_url, @duracion_min, @descripcion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@curso_id", leccion.Curso_id);
                    command.Parameters.AddWithValue("@titulo", leccion.Titulo);
                    command.Parameters.AddWithValue("@contenido_url", leccion.ContenidoUrl);
                    command.Parameters.AddWithValue("@duracion_min", leccion.DuracionMin);
                    command.Parameters.AddWithValue("@descripcion", leccion.Descripción);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(UpdateLeccionDTO leccion, int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Leccion SET titulo = @titulo, contenido_url = @contenido_url, duracion_min = @duracion_min, descripcion = @descripcion WHERE leccion_id = @leccion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@titulo", leccion.Titulo);
                    command.Parameters.AddWithValue("@contenido_url", leccion.ContenidoUrl);
                    command.Parameters.AddWithValue("@duracion_min", leccion.DuracionMin);
                    command.Parameters.AddWithValue("@descripcion", leccion.Descripción);
                    command.Parameters.AddWithValue("@leccion_id", id);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Leccion WHERE leccion_id = @leccion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@leccion_id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}