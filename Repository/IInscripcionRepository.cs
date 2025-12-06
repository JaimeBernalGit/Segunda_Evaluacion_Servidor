using Models;
namespace CursosAPI.Repositories
{
    public interface IInscripcionRepository
    {
        Task<List<Inscripcion>> GetAllAsync();
        Task<Inscripcion?> GetByIdAsync(int id);
        Task AddAsync(CreateInscripcionDTO inscripcion);
        Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id);
        Task DeleteAsync(int id);
    }
}
