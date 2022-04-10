namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Models;

public class LoginWithUsernameRequest
{
    public LoginWithUsernameRequest(string Username, string Password)
    {
        this.Username = Username;        
        this.Password = Password;
    }



    public string Username { get; }
    public string Password { get; }
}