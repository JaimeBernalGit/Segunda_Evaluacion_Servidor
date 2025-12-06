using Models;
using CursosAPI.Repositories;


namespace CursosAPI.Services
{
    public class ResenaService : IResenaService
    {

        private readonly IResenaRepository _resenaRepository;
        private readonly ICursoRepository _cursoRepository;

        private readonly IUsuarioRepository _usuarioRepository;


        public ResenaService(IResenaRepository resenaRepository, ICursoRepository cursoRepository, IUsuarioRepository usuarioRepository)
        {
            _resenaRepository = resenaRepository;
            _cursoRepository = cursoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Resena>> GetAllAsync(int? calificacion, DateTime? fecha = null)
        {

            var resenas = await _resenaRepository.GetAllAsync();

            if (calificacion.HasValue)
            {
                resenas = resenas
                    .Where(r => r.Calificacion == calificacion.Value)
                    .ToList();
            }

            if (fecha.HasValue)
            {
                resenas = resenas
                    .Where(r => r.FechaPublicacion.Date == fecha.Value.Date)
                    .ToList();
            }

            return resenas;

        }


        public async Task<Resena?> GetByIdAsync(int id)
        {
            var Resena = await _resenaRepository.GetByIdAsync(id);
            if (Resena == null || Resena.Id < 0)
            {
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
            }
            return Resena;

        }


        public async Task<List<Resena>> GetByUsuarioAndCursoAsync(int usuarioId, int cursoId)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {usuarioId} no existe.");
            }

            var curso = await _cursoRepository.GetByIdAsync(cursoId);
            if (curso == null)
            {
                throw new KeyNotFoundException($"El curso con ID {cursoId} no existe.");
            }

            var todasLasResenas = await _resenaRepository.GetAllAsync();
            var resenasFiltradas = todasLasResenas
                    .Where(r => r.Usuario.Id == usuarioId && r.Curso.Id == cursoId)
                    .ToList();

            if (resenasFiltradas.Count == 0)
            {
                throw new KeyNotFoundException($"No se encontraron reseñas del usuario {usuarioId} para el curso {cursoId}.");
            }

            return resenasFiltradas;

        }

        public async Task<List<Resena>> GetByUsuarioAsync(int usuarioId)
        {

            var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {usuarioId} no existe.");
            }

            var todasLasResenas = await _resenaRepository.GetAllAsync();

            var resenasUsuario = todasLasResenas
                .Where(r => r.Usuario.Id == usuarioId) 
                .ToList();

            if (resenasUsuario.Count == 0)
            {
                throw new KeyNotFoundException($"El usuario {usuarioId} aún no ha escrito ninguna reseña.");
            }

            return resenasUsuario;
        }

        public async Task<List<Resena>> GetByCursoAsync(int cursoId)
        {

            var curso = await _cursoRepository.GetByIdAsync(cursoId);
            if (curso == null)
            {
                throw new KeyNotFoundException($"El curso con ID {cursoId} no existe.");
            }

            var resenas = await _resenaRepository.GetAllAsync();

            var resenasCurso = resenas
                .Where(r => r.Curso.Id == cursoId) 
                .ToList();

            if (resenasCurso.Count == 0)
            {
                throw new KeyNotFoundException($"El curso {cursoId} no tiene reseñas todavía.");
            }

            return resenasCurso;
        }


        public async Task AddAsync(ResenaCreateDTO resena)
        {


            if (resena.Calificacion > 5 || resena.Calificacion < 0)
            {
                throw new InvalidOperationException($"El Resena debe tener una calificacion con valores entre 0 y 5");
            }

            if (resena.UsuarioId <= 0)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            if (resena.CursoId <= 0)
            {
                throw new InvalidOperationException("El curso no existe.");
            }

            if (string.IsNullOrWhiteSpace(resena.Comentario))
                throw new InvalidOperationException("El comentario no puede estar vacío.");

            if (resena.Comentario.Length > 1000)
                throw new InvalidOperationException("El comentario no puede superar los 1000 caracteres.");



            await _resenaRepository.AddAsync(resena);


        }

        public async Task UpdateAsync(int resenaId, ResenaUpdateDTO resenaDto)
        {
            try
            {
                var resenaExistente = await GetByIdAsync(resenaId);

                if (resenaExistente == null)
                    throw new InvalidOperationException("La resena que intentas actualizar no existe.");


                if (resenaDto.Calificacion < 1 || resenaDto.Calificacion > 5)
                    throw new InvalidOperationException("La calificación debe estar entre 1 y 5.");

                if (string.IsNullOrWhiteSpace(resenaDto.Comentario))
                    throw new InvalidOperationException("El comentario no puede estar vacío.");

                if (resenaDto.Comentario.Length > 1000)
                    throw new InvalidOperationException("El comentario no puede superar los 1000 caracteres.");

                resenaExistente.Calificacion = resenaDto.Calificacion;
                resenaExistente.Comentario = resenaDto.Comentario;


                await _resenaRepository.UpdateAsync(resenaExistente);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al actualizar la reseña.", ex);
            }


        }

        public async Task DeleteAsync(int id)
        {
            var Resena = await GetByIdAsync(id);
            if (Resena == null)
            {
                throw new KeyNotFoundException("El Resena no existe");
            }

            await _resenaRepository.DeleteAsync(id);

        }

    }

}