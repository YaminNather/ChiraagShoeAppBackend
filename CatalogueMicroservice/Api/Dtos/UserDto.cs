namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class UserDto
{
    public UserDto(String Id, String Username)
    {
        this.Id = Id;
        this.UserName = Username; 
    }

    public string Id { get; init; } = null!;

    public string UserName { get; init; } = null!;
}