using Models;

namespace CursosAPI.Repositories
{
    public interface IPagoRepository
    {
        Task<List<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task AddAsync(PagoCreateDTO pago);


    }
}
