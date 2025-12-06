namespace Models;
public class UpdateLeccionDTO
{
    public string Titulo { get; set; }
    public string ContenidoUrl { get; set; }
    public int DuracionMin { get; set; }
    public string Descripción { get; set; }

    public UpdateLeccionDTO(){}

    public UpdateLeccionDTO(string titulo, string contenidoUrl, int duracionMin, string descripción)
    {
        Titulo = titulo;
        ContenidoUrl = contenidoUrl;
        DuracionMin = duracionMin;
        Descripción = descripción;

    }
}