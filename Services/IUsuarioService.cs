using Models;
namespace CursosAPI.Services
{
    public interface IUsuarioService
    {
        Task<List<UserDtoOut>> GetAllAsync(string? nombre = null, string? estado = null, DateTime? fechaRegistroDesde = null, DateTime? fechaRegistroHasta = null, string? orderBy = null, bool descending = false);
        Task<UserDtoOut?> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDTO usuario);
        Task UpdateAsync(UpdateUsuarioDTO usuario, int id);
        Task DeleteAsync(int id);


    }
}
