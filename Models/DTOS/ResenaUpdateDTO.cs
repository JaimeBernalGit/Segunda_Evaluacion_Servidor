namespace Models;

public class ResenaUpdateDTO
{
    public int Calificacion { get; set; }
    public string Comentario { get; set; }


    public ResenaUpdateDTO() { }


    public ResenaUpdateDTO(int calificacion, string comentario)
    {
        Calificacion = calificacion;
        Comentario = comentario;

    }


}
