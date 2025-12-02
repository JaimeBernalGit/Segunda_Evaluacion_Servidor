using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly string _connectionString;

        public PagoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Pago>> GetAllAsync()
        {
          
        }

        public async Task<Pago> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Pago pago)
        {
           
        }

        public async Task UpdateAsync(Pago pago)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}