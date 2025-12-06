namespace Models;
public class CreateLeccionDTO
{
    public int Curso_id { get; set; }
    public string Titulo { get; set; }
    public string ContenidoUrl { get; set; }
    public int DuracionMin { get; set; }
    public string Descripción { get; set; }

    public CreateLeccionDTO(){}

    public CreateLeccionDTO(int curso_id, string titulo, string contenidoUrl, int duracionMin, string descripción)
    {
        Curso_id = curso_id;
        Titulo = titulo;
        ContenidoUrl = contenidoUrl;
        DuracionMin = duracionMin;
        Descripción = descripción;

    }
}