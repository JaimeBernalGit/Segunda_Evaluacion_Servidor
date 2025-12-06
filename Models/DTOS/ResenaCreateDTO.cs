namespace Models;

public class ResenaCreateDTO
{
    public int UsuarioId { get; set; } 
    public int CursoId { get; set; }

    public int Calificacion { get; set; }
    public string Comentario { get; set; }

    public ResenaCreateDTO() { }


    public ResenaCreateDTO(int usuarioId, int cursoId, int calificacion, string comentario)
    {
        UsuarioId = usuarioId;
        CursoId = cursoId;
        Calificacion = calificacion;
        Comentario = comentario;

    }



}
