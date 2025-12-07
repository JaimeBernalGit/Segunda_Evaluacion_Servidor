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
                lecciones = lecciones.Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (duracionMinima.HasValue)
            {
                lecciones = lecciones.Where(l => l.DuracionMin >= duracionMinima.Value).ToList();
            }

            if (duracionMaxima.HasValue)
            {
                lecciones = lecciones.Where(l => l.DuracionMin <= duracionMaxima.Value).ToList();
            }

            return lecciones;
        }

        public async Task<Leccion?> GetByIdAsync(int id)
        {
            var leccion = await _leccionRepository.GetByIdAsync(id);
            if (leccion == null || leccion.Id < 0)
            {
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
            }
            return leccion;
        }

        public async Task AddAsync(CreateLeccionDTO leccion)
        {
            if (leccion.Curso_id <= 0)
            {
                throw new InvalidOperationException("El curso no existe.");
            }

            if (string.IsNullOrWhiteSpace(leccion.Titulo))
            {
                throw new InvalidOperationException("El título no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(leccion.ContenidoUrl))
            {
                throw new InvalidOperationException("La URL del contenido no puede estar vacía.");
            }

            if (leccion.DuracionMin <= 0)
            {
                throw new InvalidOperationException("La duración debe ser mayor a 0 minutos.");
            }

            await _leccionRepository.AddAsync(leccion);
        }

        public async Task UpdateAsync(UpdateLeccionDTO leccion, int id)
        {
            var leccionExistente = await GetByIdAsync(id);
            
            if (leccionExistente == null)
            {
                throw new KeyNotFoundException("La lección no existe.");
            }

            if (string.IsNullOrWhiteSpace(leccion.Titulo))
            {
                throw new InvalidOperationException("El título no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(leccion.ContenidoUrl))
            {
                throw new InvalidOperationException("La URL del contenido no puede estar vacía.");
            }

            if (leccion.DuracionMin <= 0)
            {
                throw new InvalidOperationException("La duración debe ser mayor a 0 minutos.");
            }

            await _leccionRepository.UpdateAsync(leccion, id);
        }

        public async Task DeleteAsync(int id)
        {
            var leccion = await GetByIdAsync(id);
            if (leccion == null)
            {
                throw new KeyNotFoundException("La lección no existe.");
            }

            await _leccionRepository.DeleteAsync(id);
        }
    }
}
