using Models;

namespace CursosAPI.Services
{
    public interface ICursoService
    {
        Task<List<Curso>> GetAllAsync(string? titulo = null, string? categoria = null, string? orderBy = null, bool descending = false);
        Task<Curso?> GetByIdAsync(int id);
        Task AddAsync(CursoCreateDTO curso);
        Task UpdateAsync(int id, CursoUpdateDTO curso);
        Task DeleteAsync(int id);

    }
}
