namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class UserDto
{
    public UserDto(String Id, String Username)
    {
        this.Id = Id;
        this.Username = Username; 
    }

    public string Id { get; init; } = null!;

    public string Username { get; init; } = null!;
}