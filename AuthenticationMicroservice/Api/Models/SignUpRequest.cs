using Newtonsoft.Json;
using Microsoft.AspNetCore;

public class SignUpRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}