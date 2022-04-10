namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class ConfirmBidRequest
{
    public string Product { get; init; } = null!;
    public string Bidder { get; init; } = null!;
    public string DeliverTo { get; init; } = null!;
}