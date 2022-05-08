namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class AcceptBidRequest
{
    public string Product { get; init; } = null!;
    public string Bidder { get; init; } = null!;
}