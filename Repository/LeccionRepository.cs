using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class LeccionRepository : ILeccionRepository
    {
        private readonly string _connectionString;

        public LeccionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Leccion>> GetAllAsync()
        {
          
        }

        public async Task<Leccion> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Leccion leccion)
        {
           
        }

        public async Task UpdateAsync(Leccion leccion)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}