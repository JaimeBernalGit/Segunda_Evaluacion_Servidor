namespace Models;
public class Pago{

    public int Id { get; set; }
    public Usuario Usuario { get; set; }
    public Curso Curso { get; set; }
    public double Cantidad { get; set; }
    public string Nombre_Titular{ get; set; }
    public string Numero_Titular { get; set; }
    public string CVV { get; set; }

    public Pago(){}

    public Pago(int id, Usuario usuario, Curso curso, double cantidad, string nombre_Titular, string numero_Titular, string cVV)
    {
        Id = id;
        Usuario = usuario;
        Curso = curso;
        Cantidad = cantidad;
        Nombre_Titular = nombre_Titular;
        Numero_Titular = numero_Titular;
        CVV = cVV;
    }
}