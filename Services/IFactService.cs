namespace CursosAPI.Services;
public interface IFactService
{
    Task<string> GetRandomFactAsync();
}