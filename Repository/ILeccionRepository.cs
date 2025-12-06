using Models;
namespace CursosAPI.Repositories
{
    public interface ILeccionRepository
    {
        Task<List<Leccion>> GetAllAsync();
        Task<Leccion?> GetByIdAsync(int id);
        Task AddAsync(CreateLeccionDTO leccion);
        Task UpdateAsync(UpdateLeccionDTO leccion, int id);
        Task DeleteAsync(int id);
    }
}
