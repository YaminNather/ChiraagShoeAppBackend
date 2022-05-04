namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class AddProductRequest
{
    public string Name { get; set; } = null!;
    
    public float Price { get; set; }
    
    public string Description { get; set; } = null!;

    public int Size { get; set; }
}