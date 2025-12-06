using Models;

namespace CursosAPI.Repositories
{
    public interface IResenaRepository
    {
        Task<List<Resena>> GetAllAsync();
        Task<Resena?> GetByIdAsync(int id);
        Task AddAsync(ResenaCreateDTO resena);
        Task UpdateAsync(Resena resena);
        Task DeleteAsync(int id);

    }
}
