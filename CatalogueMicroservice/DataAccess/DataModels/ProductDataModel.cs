using Supabase;
using Postgrest.Attributes;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

[Table("products")]
public class ProductDataModel : SupabaseModel
{
    // public ProductDataModel(int Id, String Name, String Description, float InitialPrice, DateTime CreatedAt, DateTime ModifiedAt)
    // {
    //     this.Id = Id;
    //     this.Name = Name;
    //     this.Description = Description;
    //     this.InitialPrice = InitialPrice;
    //     this.CreatedAt = CreatedAt;
    //     this.ModifiedAt = ModifiedAt;
    // }

    [PrimaryKey("id", shouldInsert: false)]
    public int? Id { get; init; } = null;
    
    [Column("name")]
    public string Name { get; init; } = null!;

    [Column("seller")]
    public string Seller { get; init; } = null!;

    [Column("description")]
    public string Description { get; init; } = null!;

    // [Column("category")]
    // public string? Category { get; init; }

    [Column("initial_price")]
    public float InitialPrice { get; init; }

    [Column("created_at")]
    public DateTime CreatedAt { get; init; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; init; }

    [Column("main_image")]
    public String MainImage { get; init; } = null!;
    
    [Column("images")]
    public List<string>? Images { get; init; } = null!;

    [Column("is_available")]
    public bool IsAvailable { get; init; }
}