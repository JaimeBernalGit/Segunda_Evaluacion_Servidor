using Models;
namespace CursosAPI.Services
{
    public interface ILeccionService
    {
        Task<List<Leccion>> GetAllAsync(string? titulo = null, int? duracionMinima = null, int? duracionMaxima = null);
        Task<Leccion?> GetByIdAsync(int id);
        Task AddAsync(CreateLeccionDTO leccion);
        Task UpdateAsync(UpdateLeccionDTO leccion, int id);
        Task DeleteAsync(int id);


    }
}
