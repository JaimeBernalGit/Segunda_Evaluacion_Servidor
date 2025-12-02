namespace CursosAPI.Services
{
    public interface ILeccionService
    {
        Task<List<Leccion>> GetAllAsync();
        Task<Leccion?> GetByIdAsync(int id);
        Task AddAsync(Leccion leccion);
        Task UpdateAsync(Leccion leccion);
        Task DeleteAsync(int id);


    }
}
