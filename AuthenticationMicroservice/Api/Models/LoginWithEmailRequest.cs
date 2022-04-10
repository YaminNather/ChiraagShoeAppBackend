namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Models;

public class LoginWithEmailRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}