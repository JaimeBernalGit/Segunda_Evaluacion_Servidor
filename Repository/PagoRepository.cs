using Models;
using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly string _connectionString;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICursoRepository _cursoRepository;


        public PagoRepository(IConfiguration configuration, IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository)
        {
            _connectionString = configuration.GetConnectionString("GestionCursosDB") ?? "Not found";
            _usuarioRepository = usuarioRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            var Pagos = new List<Pago>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT pago_id, usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv FROM Pago";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var Pago = new Pago
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                Cantidad = Convert.ToDouble(reader.GetDecimal(3)),
                                Nombre_Titular = reader.GetString(4),
                                Numero_Titular = reader.GetString(5),
                                CVV = reader.GetString(6),
                            };

                            Pagos.Add(Pago);
                        }
                    }
                }
            }
            return Pagos;
        }

        public async Task<Pago> GetByIdAsync(int id)
        {
            Pago Pago = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT pago_id, usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv FROM Pago WHERE pago_id = @pago_id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@pago_id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Pago = new Pago
                            {
                                Id = reader.GetInt32(0),
                                Usuario = await _usuarioRepository.GetByIdAsync(reader.GetInt32(1)),
                                Curso = await _cursoRepository.GetByIdAsync(reader.GetInt32(2)),
                                Cantidad = Convert.ToDouble(reader.GetDecimal(3)),
                                Nombre_Titular = reader.GetString(4),
                                Numero_Titular = reader.GetString(5),
                                CVV = reader.GetString(6),
                            };
                        }
                    }
                }
            }
            return Pago;
        }

        public async Task AddAsync(PagoCreateDTO Pago) 
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Pago (usuario_id, curso_id, cantidad, nombre_titular, numero_titular, cvv) VALUES (@usuario_id, @curso_id, @cantidad, @nombre_titular, @numero_titular, @cvv);";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario_id", Pago.UsuarioId);
                    command.Parameters.AddWithValue("@curso_id", Pago.CursoId);
                    command.Parameters.AddWithValue("@cantidad", Pago.Cantidad);
                    command.Parameters.AddWithValue("@nombre_titular", Pago.Nombre_Titular);
                    command.Parameters.AddWithValue("@numero_titular", Pago.Numero_Titular);
                    command.Parameters.AddWithValue("@cvv", Pago.CVV);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }

}