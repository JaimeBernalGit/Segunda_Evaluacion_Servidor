using Models;
using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            var Cursos = new List<Curso>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Curso";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Curso = new Curso
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Categoria = reader.GetString(3),
                                Nivel = reader.GetString(4),
                                Fecha_Creacion = reader.GetString(4),

                            }; 

                            Cursos.Add(Curso);
                        }
                    }
                }
            }
            return Cursos;
        }

        public async Task<Curso> GetByIdAsync(int id)
        {
            Curso Curso = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Precio, EsAlcoholica FROM Curso WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Curso = new Curso
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = Convert.ToDouble(reader.GetDecimal(2)),
                                EsAlcoholica = reader.GetBoolean(3)
                            };
                        }
                    }
                }
            }
            return Curso;
        }

        public async Task AddAsync(Curso Curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Curso (Nombre, Precio, EsAlcoholica) VALUES (@Nombre, @Precio, @EsAlcoholica)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Curso.Nombre);
                    command.Parameters.AddWithValue("@Precio", Curso.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", Curso.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Curso Curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Curso SET Nombre = @Nombre, Precio = @Precio, EsAlcoholica = @EsAlcoholica WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Curso.Id);
                    command.Parameters.AddWithValue("@Nombre", Curso.Nombre);
                    command.Parameters.AddWithValue("@Precio", Curso.Precio);
                    command.Parameters.AddWithValue("@EsAlcoholica", Curso.EsAlcoholica);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Curso WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        


    }

}