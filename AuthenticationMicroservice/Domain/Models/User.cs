namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;

public class User
{
    public User(string Id, string Username, string Email, string Password) {
        this.Id = Id;
        this.Username = Username;
        this.Email = Email;
        this.Password = Password;
    }

    public string Id { get; }

    public string Username { get; }
    
    public string Email { get; }

    public string Password { get; }
}