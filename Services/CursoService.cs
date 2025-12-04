using Models;
using CursosAPI.Repositories;

namespace CursosAPI.Services
{
    public class CursoService : ICursoService
    {

        private readonly ICursoRepository _cursoRepository ;


        public CursoService(ICursoRepository cursoRepository)
        {
           _cursoRepository = cursoRepository;
        }

        public Task AddAsync(CursoCreateDTO curso)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Curso>> GetAllAsync()
        {
            var cursos = await _cursoRepository.GetAllAsync();
            return cursos;
    
        }

        public Task<Curso?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Curso curso)
        {
            throw new NotImplementedException();
        }
    }

}