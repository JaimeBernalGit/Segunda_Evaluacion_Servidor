using Models;
namespace CursosAPI.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<UserDtoOut>> GetAllAsync();
        Task<UserDtoOut?> GetByIdAsync(int id);
        Task AddAsync(UserInDto usuario);
        Task UpdateAsync(UpdateUsuarioInDTO usuario, int id);
        Task DeleteAsync(int id);
        Task<UserDtoOut> AddUserFromCredentials(UserInDto userInDto);
        Task<UserDtoOut?> GetUserFromCredentials(LoginDTO loginDTO);
    }
}
