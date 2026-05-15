using Models;
namespace CursosAPI.Services
{
    public interface IInscripcionService
    {
        Task<List<Inscripcion>> GetAllAsync(string? estado = null, int? progresoMinimo = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null);
        Task<Inscripcion?> GetByIdAsync(int id);
        Task<List<Inscripcion>> GetByUsuarioAsync(int usuarioId);
        Task AddAsync(CreateInscripcionDTO inscripcion);
        Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id);
        Task DeleteAsync(int id);
    }
}
