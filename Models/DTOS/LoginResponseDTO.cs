namespace Models;

public class LoginResponseDTO
{
    public string Token { get; set; }
    public string Fact { get; set; }

    public LoginResponseDTO(string token, string fact)
    {
        Token = token;
        Fact = fact;
    }

    public LoginResponseDTO(){}

}