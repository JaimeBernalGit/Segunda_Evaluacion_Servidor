using Models;

namespace CursosAPI.Services
{
    public interface IPagoService
    {
        Task<List<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task<List<Pago>> GetByUsuarioAsync(int usuarioId);
        Task<List<Pago>> GetByCursoAsync(int cursoId);
        Task AddAsync(PagoCreateDTO Pago);


    }
}
