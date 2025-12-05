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

                string query = "SELECT curso_id, titulo, descripcion, categoria, nivel, fecha_creacion, precio FROM Curso";
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
                                Fecha_Creacion = reader.GetDateTime(5),
                                Precio = Convert.ToDouble(reader.GetDecimal(6))

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

                string query = "SELECT curso_id, titulo, descripcion, categoria, nivel, fecha_creacion, precio FROM Curso WHERE curso_id = @curso_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@curso_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Curso = new Curso
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Descripcion = reader.GetString(2),
                                Categoria = reader.GetString(3),
                                Nivel = reader.GetString(4),
                                Fecha_Creacion = reader.GetDateTime(5),
                                Precio = Convert.ToDouble(reader.GetDecimal(6))
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

                string query = "INSERT INTO Curso (titulo, descripcion, categoria, nivel, precio) VALUES (@titulo, @descripcion, @categoria, @nivel, @precio);";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@titulo", Curso.Titulo);
                    command.Parameters.AddWithValue("@descripcion", Curso.Descripcion);
                    command.Parameters.AddWithValue("@categoria", Curso.Categoria);
                    command.Parameters.AddWithValue("@nivel", Curso.Nivel);
                    command.Parameters.AddWithValue("@precio", Curso.Precio);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Curso Curso)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Curso SET titulo = @titulo, descripcion = @descripcion, categoria = @categoria, nivel = @nivel, precio = @precio WHERE curso_id = @curso_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@curso_id", Curso.Id);
                    command.Parameters.AddWithValue("@titulo", Curso.Titulo);
                    command.Parameters.AddWithValue("@descripcion", Curso.Descripcion);
                    command.Parameters.AddWithValue("@categoria", Curso.Categoria);
                    command.Parameters.AddWithValue("@nivel", Curso.Nivel);
                    command.Parameters.AddWithValue("@precio", Curso.Precio);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Curso WHERE curso_id = @curso_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@curso_id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }




    }

}