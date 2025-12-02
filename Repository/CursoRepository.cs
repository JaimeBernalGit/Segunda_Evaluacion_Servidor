using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly string _connectionString;

        public CursoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Curso>> GetAllAsync()
        {
          
        }

        public async Task<Curso> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Curso curso)
        {
           
        }

        public async Task UpdateAsync(Curso curso)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}