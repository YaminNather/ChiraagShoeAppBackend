namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class ConfirmPaymentRequest 
{
    public String Product { get; init; } = null!;
}