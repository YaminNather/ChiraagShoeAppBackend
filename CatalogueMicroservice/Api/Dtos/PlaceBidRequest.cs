[System.Serializable]
public class PlaceBidRequest
{
    public string Bidder { get; init; } = null!;

    public string ProductId { get; init; } = null!;

    public float Amount { get; init; }
}