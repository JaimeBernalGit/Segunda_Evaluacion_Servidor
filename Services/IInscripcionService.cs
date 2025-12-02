namespace CursosAPI.Services
{
    public interface IInscripcionService
    {
        Task<List<Inscripcion>> GetAllAsync();
        Task<Inscripcion?> GetByIdAsync(int id);
        Task AddAsync(Inscripcion inscripcion);
        Task UpdateAsync(Inscripcion inscripcion);
        Task DeleteAsync(int id);
    }
}
