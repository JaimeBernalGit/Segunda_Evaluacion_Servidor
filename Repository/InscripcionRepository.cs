using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class InscripcionRepository : IInscripcionRepository
    {
        private readonly string _connectionString;

        public InscripcionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
          
        }

        public async Task<Inscripcion> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Inscripcion inscripcion)
        {
           
        }

        public async Task UpdateAsync(Inscripcion inscripcion)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}