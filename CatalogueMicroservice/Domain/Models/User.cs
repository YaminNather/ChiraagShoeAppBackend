namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class User
{
    public User(string Id, string Username)
    {
        this.Id = Id;
        this.Username = Username;
    }


    public readonly string Id;
    public readonly string Username;
}