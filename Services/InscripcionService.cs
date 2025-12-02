using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public InscripcionService(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
            return await _inscripcionRepository.GetAllAsync();
        }

        public async Task<Inscripcion?> GetByIdAsync(int id)
        {
            return await _inscripcionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Inscripcion inscripcion)
        {
            await _inscripcionRepository.AddAsync(inscripcion);
        }

        public async Task UpdateAsync(Inscripcion inscripcion)
        {
            await _inscripcionRepository.UpdateAsync(inscripcion);
        }

        public async Task DeleteAsync(int id)
        {
            await _inscripcionRepository.DeleteAsync(id);
        }
    }
}
