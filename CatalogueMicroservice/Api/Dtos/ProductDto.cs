namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class ProductDto
{

    public string Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Seller { get; init; } = null!;
    
    public string Description { get; init; } = null!;

    public string? Category { get; init; } = null;
    
    public float InitialPrice { get; init; }

    public string MainImage { get; init; } = null!;

    public List<string>? Images { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime ModifiedAt { get; init; }

    public bool IsAvailable { get; init; }
}