using Models;
using CursosAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

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
            var cursos = await _cursoRepository.GetAllAsync();
            return cursos;

        }


        public async Task<Curso?> GetByIdAsync(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso == null || curso.Id < 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero o no existe.");
            }
            return curso;

        }

        public async Task AddAsync(CursoCreateDTO curso)
        {
            var cursosOnline = await _cursoRepository.GetAllAsync();

            foreach (var cur in cursosOnline)
            {
                if (cur.Titulo.Equals(curso.Titulo))
                {
                    throw new InvalidOperationException($"El curso '{curso.Titulo}' ya existe en el catálogo.");
                }
            }

            if (curso.Precio <= 0)
            {
                throw new InvalidOperationException($"El curso '{curso.Titulo}' no puede tener un precio menor o igual a 0");
            }

            await _cursoRepository.AddAsync(curso);


        }

        public async Task UpdateAsync(Curso curso)
        {
            if (curso.Id <= 0)
            {
                throw new ArgumentException("El ID del curso no es válido.");
            }

            if(curso.Precio <= 0)
            {
                throw new InvalidOperationException($"El precio no puede ser menor o igual a 0.");
            }

            await GetByIdAsync(curso.Id);



            var cursosOnline = await GetAllAsync();

            foreach (var cur in cursosOnline)
            {
                if (cur.Titulo.Equals(curso.Titulo))
                {
                    throw new InvalidOperationException($"El curso '{curso.Titulo}' ya existe en el catálogo.");
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            await GetByIdAsync(id);

            await _cursoRepository.DeleteAsync(id);
        }

    }

}