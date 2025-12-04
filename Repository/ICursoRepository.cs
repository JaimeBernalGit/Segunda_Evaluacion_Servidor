using Models;

namespace CursosAPI.Repositories
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(int id);
        Task <int>AddAsync(CursoCreateDTO curso);
        Task UpdateAsync(Curso curso);
        Task DeleteAsync(int id);

    }
}
