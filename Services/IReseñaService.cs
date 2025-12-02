namespace CursosAPI.Services
{
    public interface IReseñaService
    {
        Task<List<Reseña>> GetAllAsync();
        Task<Reseña?> GetByIdAsync(int id);
        Task AddAsync(Reseña reseña);
        Task UpdateAsync(Reseña reseña);
        Task DeleteAsync(int id);


    }
}
