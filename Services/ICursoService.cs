using Models;

namespace CursosAPI.Services
{
    public interface ICursoService
    {
        Task<List<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(int id);
        Task AddAsync(CursoCreateDTO curso);
        Task UpdateAsync(int id, CursoUpdateDTO curso);
        Task DeleteAsync(int id);

    }
}
