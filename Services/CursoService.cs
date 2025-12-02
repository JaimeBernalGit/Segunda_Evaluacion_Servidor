using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
            
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            return await _cursoRepository.GetAllAsync();
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            return await _cursoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Curso curso)
        {
            await _cursoRepository.AddAsync(curso);
        }

        public async Task UpdateAsync(Curso curso)
        {
            await _cursoRepository.UpdateAsync(curso);
        }

        public async Task DeleteAsync(int id)
        {
            await _cursoRepository.DeleteAsync(id);
        }
    }
}
