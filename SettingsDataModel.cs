using Postgrest.Attributes;
using Supabase;

[Table("settings")]
class SettingsDataModel : SupabaseModel
{
    [PrimaryKey("name")]
    public string Name { get; } = null!; 

    [PrimaryKey("value")]
    public string? KeyValue { get; }
}