namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class AddProductRequest
{
    public string Name { get; set; } = null!;

    public string Seller { get; set; } = null!;
    
    public float InitialPrice { get; set; }
        
    public string Description { get; set; } = null!;

    public int Size { get; set; }

    public string MainImage { get; set; } = null!;

    public List<string>? Images { get; set; } = null!;
}