using Models;

namespace CursosAPI.Services
{
    public interface IResenaService
    {
        Task<List<Resena>> GetAllAsync(int? calificacion, DateTime? fecha = null);
        Task<Resena?> GetByIdAsync(int id);
        Task AddAsync(ResenaCreateDTO resena);
        Task UpdateAsync(int id, ResenaUpdateDTO resena);
        Task DeleteAsync(int id);

    }
}
