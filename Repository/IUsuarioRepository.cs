using Models;
namespace CursosAPI.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<UserDtoOut>> GetAllAsync();
        Task<UserDtoOut?> GetByIdAsync(int id);
        Task AddAsync(CreateUsuarioDTO usuario);
        Task UpdateAsync(UpdateUsuarioDTO usuario, int id);
        Task DeleteAsync(int id);
        Task<UserDtoOut> AddUserFromCredentials(RegisterDTO registerDTO);
        Task<UserDtoOut?> GetUserFromCredentials(LoginDTO loginDTO);
    }
}
