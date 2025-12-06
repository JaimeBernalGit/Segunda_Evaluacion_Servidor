namespace Models;
public class UpdateInscripcionDTO
{
    public int ProgresoPorcentaje { get; set; }
    public string Estado { get; set; }

    public UpdateInscripcionDTO(){}

    public UpdateInscripcionDTO(int progresoPorcentaje, string estado)
    {
        ProgresoPorcentaje = progresoPorcentaje;
        Estado = estado;
    }
}