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
                throw new KeyNotFoundException("El ID debe ser mayor que cero o no existe.");
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

            //Se crea un nuevo objeto curso que contenga todo lo que necesita base de datos
            var cursoNuevo = new Curso
            {
                Titulo = curso.Titulo,
                Descripcion = curso.Descripcion,
                Categoria = curso.Categoria,
                Nivel = curso.Nivel,
                Precio = curso.Precio,
                Fecha_Creacion = DateTime.Now
            };

            await _cursoRepository.AddAsync(cursoNuevo);


        }

        public async Task UpdateAsync(int id, CursoUpdateDTO curso)
        {
            try
            {
                if (curso.Precio <= 0)
                {
                    throw new InvalidOperationException($"El precio no puede ser menor o igual a 0.");
                }

                var cursosOnline = await _cursoRepository.GetAllAsync();

    
                foreach (var cur in cursosOnline)
                {
                    if (cur.Titulo.Equals(curso.Titulo) && cur.Id != id)
                    {
        
                        throw new InvalidOperationException($"El curso '{curso.Titulo}' ya existe en el catálogo.");
                    }
                }

                var cursoExistente = await GetByIdAsync(id);

                cursoExistente.Titulo = curso.Titulo;
                cursoExistente.Descripcion = curso.Descripcion;
                cursoExistente.Categoria = curso.Categoria;
                cursoExistente.Nivel = curso.Nivel;
                cursoExistente.Precio = curso.Precio;

                await _cursoRepository.UpdateAsync(cursoExistente);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            // Si el servicio no encontró el curso para actualizar
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }

        }

        public async Task DeleteAsync(int id)
        {

            try
            {
                await GetByIdAsync(id);

                await _cursoRepository.DeleteAsync(id);
            }

            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

    }

}