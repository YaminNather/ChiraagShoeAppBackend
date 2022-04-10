using Postgrest.Attributes;
using Supabase;

[Table("users")]
public class UserDataModel : SupabaseModel
{
    [PrimaryKey("id")]
    public string Id { get; init; } = null!;

    [PrimaryKey("username")]
    public string Username { get; init; } = null!;

    [PrimaryKey("email")]
    public string Email { get; init; } = null!;
    
    [PrimaryKey("password")]
    public string Password { get; init; } = null!;
}