using Models;
namespace CursosAPI.Services
{
    public interface IInscripcionService
    {
        Task<List<Inscripcion>> GetAllAsync();
        Task<Inscripcion?> GetByIdAsync(int id);
        Task AddAsync(CreateInscripcionDTO inscripcion);
        Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id);
        Task DeleteAsync(int id);
    }
}
