using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class LeccionService : ILeccionService
    {
        private readonly ILeccionRepository _leccionRepository;

        public LeccionService(ILeccionRepository leccionRepository)
        {
            _leccionRepository = leccionRepository;
            
        }

        public async Task<List<Leccion>> GetAllAsync()
        {
            return await _leccionRepository.GetAllAsync();
        }

        public async Task<Leccion?> GetByIdAsync(int id)
        {
            return await _leccionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Leccion leccion)
        {
            await _leccionRepository.AddAsync(leccion);
        }

        public async Task UpdateAsync(Leccion leccion)
        {
            await _leccionRepository.UpdateAsync(leccion);
        }

        public async Task DeleteAsync(int id)
        {
            await _leccionRepository.DeleteAsync(id);
        }
    }
}
