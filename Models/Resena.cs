namespace Models;

public class Resena
{
    public int Id { get; set; }

    public UserDtoOut? Usuario { get; set; } 
    public Curso? Curso { get; set; }

    public int Calificacion { get; set; }
    public string Comentario { get; set; }
    public DateTime FechaPublicacion { get; set; }

    public Resena() { }


    public Resena(UserDtoOut usuario, Curso curso, int calificacion, string comentario, DateTime fechaPublicacion)
    {
        Usuario = usuario;
        Curso = curso;
        Calificacion = calificacion;
        Comentario = comentario;
        FechaPublicacion = fechaPublicacion;
    }



}
