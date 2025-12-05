namespace CursosAPI.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDTO usuario);
        Task UpdateAsync(UpdateUsuarioDTO usuario, int id);
        Task DeleteAsync(int id);


    }
}
