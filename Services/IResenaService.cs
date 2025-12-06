using Models;

namespace CursosAPI.Services
{
    public interface IResenaService
    {
        Task<List<Resena>> GetAllAsync(int? calificacion, DateTime? fecha = null);
        Task<Resena?> GetByIdAsync(int id);
        Task<List<Resena>> GetByUsuarioAndCursoAsync(int usuarioId, int cursoId);
        Task<List<Resena>> GetByUsuarioAsync(int usuarioId);
        Task<List<Resena>> GetByCursoAsync(int cursoId);
        Task AddAsync(ResenaCreateDTO resena);
        Task UpdateAsync(int id, ResenaUpdateDTO resena);
        Task DeleteAsync(int id);

    }
}
