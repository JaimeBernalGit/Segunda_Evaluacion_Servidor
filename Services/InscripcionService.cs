using CursosAPI.Repositories;
using Models;
namespace CursosAPI.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public InscripcionService(IInscripcionRepository inscripcionRepository, IUsuarioRepository usuarioRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Inscripcion>> GetAllAsync(string? estado = null, int? progresoMinimo = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string? orderBy = null, bool descending = false)
        {
            var inscripciones = await _inscripcionRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(estado))
            {
                estado = estado.Trim();
                inscripciones = inscripciones.Where(i => i.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (progresoMinimo.HasValue)
                inscripciones = inscripciones.Where(i => i.ProgresoPorcentaje >= progresoMinimo.Value).ToList();

            if (fechaDesde.HasValue)
                inscripciones = inscripciones.Where(i => i.FechaInscripcion.Date >= fechaDesde.Value.Date).ToList();

            if (fechaHasta.HasValue)
                inscripciones = inscripciones.Where(i => i.FechaInscripcion.Date <= fechaHasta.Value.Date).ToList();

            inscripciones = orderBy?.ToLower() switch
            {
                "fecha"    => descending ? inscripciones.OrderByDescending(i => i.FechaInscripcion).ToList()
                                        : inscripciones.OrderBy(i => i.FechaInscripcion).ToList(),
                "progreso" => descending ? inscripciones.OrderByDescending(i => i.ProgresoPorcentaje).ToList()
                                        : inscripciones.OrderBy(i => i.ProgresoPorcentaje).ToList(),
                _          => inscripciones
            };

            return inscripciones;
        }

        public async Task<Inscripcion?> GetByIdAsync(int id)
        {
            var inscripcion = await _inscripcionRepository.GetByIdAsync(id);
            if (inscripcion == null || inscripcion.Id < 0)
            {
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
            }
            return inscripcion;
        }
        public async Task<List<Inscripcion>> GetByUsuarioAsync(int usuarioId)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {usuarioId} no existe.");
            }

            var Pagos = await _inscripcionRepository.GetAllAsync();

            var PagosUsuario = Pagos
                .Where(r => r.Usuario.Id == usuarioId)
                .ToList();

            if (PagosUsuario.Count == 0)
            {
                throw new KeyNotFoundException($"El usuario {usuarioId} aún no se ha inscrito a ningún curso.");
            }

            return PagosUsuario;
        }

        public async Task AddAsync(CreateInscripcionDTO inscripcion)
        {
            if (inscripcion.UsuarioId <= 0)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            if (inscripcion.CursoId <= 0)
            {
                throw new InvalidOperationException("El curso no existe.");
            }

            await _inscripcionRepository.AddAsync(inscripcion);
        }

        public async Task UpdateAsync(UpdateInscripcionDTO inscripcion, int id)
        {
            var inscripcionExistente = await GetByIdAsync(id);
            
            if (inscripcionExistente == null)
            {
                throw new KeyNotFoundException("La inscripción no existe.");
            }

            if (inscripcion.ProgresoPorcentaje < 0 || inscripcion.ProgresoPorcentaje > 100)
            {
                throw new InvalidOperationException("El progreso debe estar entre 0 y 100.");
            }

            if (string.IsNullOrWhiteSpace(inscripcion.Estado))
            {
                throw new InvalidOperationException("El estado no puede estar vacío.");
            }

            await _inscripcionRepository.UpdateAsync(inscripcion, id);
        }

        public async Task DeleteAsync(int id)
        {
            var inscripcion = await GetByIdAsync(id);
            if (inscripcion == null)
            {
                throw new KeyNotFoundException("La inscripción no existe.");
            }

            await _inscripcionRepository.DeleteAsync(id);
        }
    }
}
