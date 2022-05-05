using ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Mappers;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;
using Postgrest.Responses;
using Supabase;

namespace ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Repositories;

public class UserRepository
{
    public UserRepository(Client client)
    {
        this.client = client;
    }

    public async Task<User> Create(String username, String email, String password)
    {
        UserDataModel userDataModel = new UserDataModel{
            Username = username,
            Email = email,
            Password = password
        };
        ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Insert(userDataModel);

        User user = mapper.ToDomainModel(userDataModel);
        return user;
    }

    public async Task Store(User user)
    {
        await client.From<UserDataModel>().Insert(mapper.FromDomainModel(user));
    }

    public async Task<User?> Get(string id)
    {
        Postgrest.Responses.ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Filter("id", Postgrest.Constants.Operator.Equals, id).Get();

        if(response.Models.Count() == 0)
            return null;

        return mapper.ToDomainModel(response.Models[0]);

    }

    public async Task<User?> GetWithUsername(string username)
    {
        Postgrest.Responses.ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Filter("username", Postgrest.Constants.Operator.Equals, username).Get();

        if(response.Models.Count() == 0)
            return null;

        return mapper.ToDomainModel(response.Models[0]);
    }

    public async Task<User?> GetWithEmail(string email)
    {
        Postgrest.Responses.ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Filter("email", Postgrest.Constants.Operator.Equals, email).Get();

        if(response.Models.Count() == 0)
            return null;

        return mapper.ToDomainModel(response.Models[0]);
    }



    private readonly Client client;
    private readonly UserMapper mapper = new UserMapper(); 
}