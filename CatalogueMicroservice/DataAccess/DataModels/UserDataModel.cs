using Supabase;
using Postgrest.Attributes;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

[Table("users")]
public class UserDataModel : SupabaseModel
{
    [Column("id")]
    public string Id { get; init; } = null!;

    [Column("username")]
    public string Username { get; init; } = null!;
}