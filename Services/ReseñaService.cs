using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class ReseñaService : IReseñaService
    {
        private readonly IReseñaRepository _reseñaRepository;

        public ReseñaService(IReseñaRepository reseñaRepository)
        {
            _reseñaRepository = reseñaRepository;
            
        }

        public async Task<List<Reseña>> GetAllAsync()
        {
            return await _reseñaRepository.GetAllAsync();
        }

        public async Task<Reseña?> GetByIdAsync(int id)
        {
            return await _reseñaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Reseña reseña)
        {
            await _reseñaRepository.AddAsync(reseña);
        }

        public async Task UpdateAsync(Reseña reseña)
        {
            await _reseñaRepository.UpdateAsync(reseña);
        }

        public async Task DeleteAsync(int id)
        {
            await _reseñaRepository.DeleteAsync(id);
        }
    }
}
