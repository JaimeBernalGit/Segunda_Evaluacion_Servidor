using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
            
        }

        public async Task<List<Pago>> GetAllAsync()
        {
            return await _pagoRepository.GetAllAsync();
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _pagoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Pago pago)
        {
            await _pagoRepository.AddAsync(pago);
        }

        public async Task UpdateAsync(Pago pago)
        {
            await _pagoRepository.UpdateAsync(pago);
        }

        public async Task DeleteAsync(int id)
        {
            await _pagoRepository.DeleteAsync(id);
        }
    }
}
