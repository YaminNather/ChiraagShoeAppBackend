using Supabase;
using Postgrest.Attributes;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

[Table("orders")]
public class OrderDataModel : SupabaseModel
{
    [PrimaryKey("product")]    
    public string Product { get; init; } = null!;

    [PrimaryKey("purchased_by")]
    public string PurchasedBy { get; init; } = null!;

    [Column("amount")]
    public float Amount { get; init; }

    [Column("deliver_to")]
    public string? DeliverTo { get; init; }

    [Column("contact_number")]
    public string? ContactNumber { get; init; }

    [Column("status")]
    public string Status { get; init; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
}