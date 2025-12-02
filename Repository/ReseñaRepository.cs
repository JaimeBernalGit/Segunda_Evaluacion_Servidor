using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class ReseñaRepository : IReseñaRepository
    {
        private readonly string _connectionString;

        public ReseñaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Reseña>> GetAllAsync()
        {
          
        }

        public async Task<Reseña> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Reseña reseña)
        {
           
        }

        public async Task UpdateAsync(Reseña reseña)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}