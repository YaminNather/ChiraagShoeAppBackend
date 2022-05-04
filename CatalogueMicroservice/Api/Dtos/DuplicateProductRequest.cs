namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class DuplicateProductRequest
{
    public string Product { get; set; } = null!;
    public string Seller { get; set; } = null!;
    public float InitialPrice { get; set; }
}