namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class CompleteCheckoutRequest
{
    public String Product { get; set; } = null!;
    
    public String Address { get; set; } = null!;

    public String ContactNumber { get; set; } = null!;     
}