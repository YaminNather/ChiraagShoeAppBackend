namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class SellBidRequest
{
    public string Seller { get; init; } = null!;
    public string ProductId { get; init; } = null!;
    public float Amount { get; init; }
}