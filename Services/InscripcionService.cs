using CursosAPI.Repositories;
using Models;
namespace CursosAPI.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public InscripcionService(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            
        }

        public async Task<List<Inscripcion>> GetAllAsync(string? estado = null, int? progresoMinimo = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var inscripciones = await _inscripcionRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(estado))
            {
                estado = estado.Trim();
                inscripciones = inscripciones
                    .Where(i => i.Estado.Equals(estado))
                    .ToList();
            }

            if (progresoMinimo.HasValue)
            {
                inscripciones = inscripciones
                    .Where(i => i.ProgresoPorcentaje >= progresoMinimo.Value)
                    .ToList();
            }

            if (fechaDesde.HasValue)
            {
                inscripciones = inscripciones
                    .Where(i => i.FechaInscripcion.Date >= fechaDesde.Value.Date)
                    .ToList();
            }

            if (fechaHasta.HasValue)
            {
                inscripciones = inscripciones
                    .Where(i => i.FechaInscripcion.Date <= fechaHasta.Value.Date)
                    .ToList();
            }

            return inscripciones;
        }

        public async Task<Inscripcion?> GetByIdAsync(int id)
        {
            return await _inscripcionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateInscripcionDTO inscripcion)
        {
            await _inscripcionRepository.AddAsync(inscripcion);
        }

        public async Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id)
        {
            await _inscripcionRepository.UpdateAsync(inscripcion, id);
        }

        public async Task DeleteAsync(int id)
        {
            await _inscripcionRepository.DeleteAsync(id);
        }
    }
}
