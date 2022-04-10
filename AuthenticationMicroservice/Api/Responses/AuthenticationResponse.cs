namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Responses;

public class AuthenticationResponse
{
    public AuthenticationResponse(string Id, string Email, string? message = null)
    {
        this.Id = Id;
        this.Email = Email;
        this.message = message;
    }

    public string Id { get; }

    public string Email { get; }

    public string? message { get; }
}