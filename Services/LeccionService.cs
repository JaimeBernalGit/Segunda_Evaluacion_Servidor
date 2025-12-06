using CursosAPI.Repositories;
using Models;
namespace CursosAPI.Services
{
    public class LeccionService : ILeccionService
    {
        private readonly ILeccionRepository _leccionRepository;

        public LeccionService(ILeccionRepository leccionRepository)
        {
            _leccionRepository = leccionRepository;
            
        }

        public async Task<List<Leccion>> GetAllAsync(string? titulo = null, int? duracionMinima = null, int? duracionMaxima = null)
        {
            var lecciones = await _leccionRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(titulo))
            {
                titulo = titulo.Trim();
                lecciones = lecciones
                    .Where(l => l.Titulo.Contains(titulo))
                    .ToList();
            }

            if (duracionMinima.HasValue)
            {
                lecciones = lecciones
                    .Where(l => l.DuracionMin >= duracionMinima.Value)
                    .ToList();
            }

            if (duracionMaxima.HasValue)
            {
                lecciones = lecciones
                    .Where(l => l.DuracionMin <= duracionMaxima.Value)
                    .ToList();
            }

            return lecciones;
        }

        public async Task<Leccion?> GetByIdAsync(int id)
        {
            return await _leccionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateLeccionDTO leccion)
        {
            await _leccionRepository.AddAsync(leccion);
        }

        public async Task UpdateAsync(UpdateLeccionDTO leccion, int id)
        {
            await _leccionRepository.UpdateAsync(leccion, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _leccionRepository.DeleteAsync(id);
        }
    }
}
