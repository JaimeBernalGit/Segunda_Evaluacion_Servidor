using Models;
namespace CursosAPI.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync(string? nombre = null, string? estado = null, DateTime? fechaRegistroDesde = null, DateTime? fechaRegistroHasta = null);
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDTO usuario);
        Task UpdateAsync(UpdateUsuarioDTO usuario, int id);
        Task DeleteAsync(int id);


    }
}
