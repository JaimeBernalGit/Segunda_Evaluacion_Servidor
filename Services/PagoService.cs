using Models;
using CursosAPI.Repositories;


namespace CursosAPI.Services
{
    public class PagoService : IPagoService
    {

        private readonly IPagoRepository _pagoRepository;
        private readonly ICursoRepository _cursoRepository;

        private readonly IUsuarioRepository _usuarioRepository;


        public PagoService(IPagoRepository PagoRepository, ICursoRepository cursoRepository, IUsuarioRepository usuarioRepository)
        {
            _pagoRepository = PagoRepository;
            _cursoRepository = cursoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Pago>> GetAllAsync()
        {

            var Pagos = await _pagoRepository.GetAllAsync();

            return Pagos;

        }


        public async Task<Pago?> GetByIdAsync(int id)
        {
            var Pago = await _pagoRepository.GetByIdAsync(id);
            if (Pago == null || Pago.Id < 0)
            {
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
            }
            return Pago;

        }


        public async Task<List<Pago>> GetByUsuarioAsync(int usuarioId)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {usuarioId} no existe.");
            }

            var Pagos = await _pagoRepository.GetAllAsync();

            var PagosUsuario = Pagos
                .Where(r => r.Usuario.Id == usuarioId)
                .ToList();

            if (PagosUsuario.Count == 0)
            {
                throw new KeyNotFoundException($"El usuario {usuarioId} aún no ha escrito ninguna reseña.");
            }

            return PagosUsuario;
        }

        public async Task<List<Pago>> GetByCursoAsync(int cursoId)
        {

            var curso = await _cursoRepository.GetByIdAsync(cursoId);
            if (curso == null)
            {
                throw new KeyNotFoundException($"El curso con ID {cursoId} no existe.");
            }

            var Pagos = await _pagoRepository.GetAllAsync();

            var PagosCurso = Pagos
                .Where(r => r.Curso.Id == cursoId)
                .ToList();

            if (PagosCurso.Count == 0)
            {
                throw new KeyNotFoundException($"El curso {cursoId} no tiene reseñas todavía.");
            }

            return PagosCurso;
        }

        public async Task AddAsync(PagoCreateDTO pago)
        {

            if (pago.UsuarioId <= 0)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            if (pago.CursoId <= 0)
            {
                throw new InvalidOperationException("El curso no existe.");
            }

            var curso = await _cursoRepository.GetByIdAsync(pago.CursoId);

            if (curso == null)
            {
                throw new KeyNotFoundException("El curso no existe.");
            }

            //Asociamos la cantidad del pago al precio del curso
            pago.Cantidad = curso.Precio;


            await _pagoRepository.AddAsync(pago);


        }


    }
}