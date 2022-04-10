using Supabase;
using Postgrest.Attributes;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

[Table("bids")]
public class BidDataModel : SupabaseModel
{
    [PrimaryKey("bidder")]
    public string Bidder { get; init; } = null!;
    
    [PrimaryKey("product_id")]
    public string ProductId { get; init; } = null!;
    
    [Column("amount")]
    public float Amount { get; init; }

    [Column("status")]
    public string Status { get; init; } = null!;
}