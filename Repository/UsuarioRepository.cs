using Microsoft.Data.SqlClient;

namespace CursosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CursosDB") ?? "Not found";
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
          
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            
        }

        public async Task AddAsync(Usuario usuario)
        {
           
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            
        }

        public async Task DeleteAsync(int id)
        {
            
        }


    }

}