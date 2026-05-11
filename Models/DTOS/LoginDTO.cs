namespace Models;

public class LoginDTO
{
    public string Nombre_Usuario { get; set; }
    public string Password { get; set; }

    public LoginDTO(){}
    public LoginDTO(string nombre_Usuario, string password)
    {
        Nombre_Usuario = nombre_Usuario;
        Password = password;
    }

}