using Microsoft.Data.SqlClient;
using Models;
namespace CursosAPI.Repositories
{
    public class InscripcionRepository : IInscripcionRepository
    {
        private readonly string _connectionString;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICursoRepository _cursoRepository;

        public InscripcionRepository(IConfiguration configuration, IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
            _usuarioRepository = usuarioRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
          var inscripciones = new List<Inscripcion>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT inscripcion_id, usuario_id, curso_id, fecha_inscripcion, progreso_porcentaje, estado FROM Inscripcion";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var inscripcion = new Inscripcion
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                FechaInscripcion = reader.GetDateTime(3),
                                ProgresoPorcentaje = (int)reader.GetDecimal(4),
                                Estado = reader.GetString(5)
                            }; 

                            inscripciones.Add(inscripcion);
                        }
                    }
                }
            }
            return inscripciones;
        }

        public async Task<Inscripcion> GetByIdAsync(int id)
        {
            Inscripcion inscripcion = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT inscripcion_id, usuario_id, curso_id, fecha_inscripcion, progreso_porcentaje, estado FROM Inscripcion WHERE inscripcion_id = @inscripcion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@inscripcion_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            inscripcion = new Inscripcion
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                FechaInscripcion = reader.GetDateTime(3),
                                ProgresoPorcentaje = (int)reader.GetDecimal(4),
                                Estado = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return inscripcion;
        }

        public async Task AddAsync(CreateInscripcionDTO inscripcion)
        {
           using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Inscripcion (usuario_id, curso_id, fecha_inscripcion, progreso_porcentaje, estado) VALUES (@usuario_id, @curso_id, @fecha_inscripcion, @progreso_porcentaje, @estado)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", inscripcion.UsuarioId);
                    command.Parameters.AddWithValue("@curso_id", inscripcion.CursoId);
                    command.Parameters.AddWithValue("@fecha_inscripcion", DateTime.Now);
                    command.Parameters.AddWithValue("@progreso_porcentaje", 0);
                    command.Parameters.AddWithValue("@estado", "Inactivo");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Inscripcion SET progreso_porcentaje = @progreso_porcentaje, estado = @estado WHERE inscripcion_id = @inscripcion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@progreso_porcentaje", inscripcion.ProgresoPorcentaje);
                    command.Parameters.AddWithValue("@estado", inscripcion.Estado);
                    command.Parameters.AddWithValue("@inscripcion_id", id);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Inscripcion WHERE inscripcion_id = @inscripcion_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@inscripcion_id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}