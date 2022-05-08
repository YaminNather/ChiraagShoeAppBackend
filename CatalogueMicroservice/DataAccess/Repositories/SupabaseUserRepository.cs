using Supabase;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;

public class SupabaseUserRepository : IUserRepository
{
    public SupabaseUserRepository(Client client, UserMapper userMapper)
    {
        this.client = client;
        this.userMapper = userMapper;
    }

    public async Task<User> Create(string username, String email, String password)
    {
        UserDataModel userDataModel = new UserDataModel{
            Username = username
        };
        ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Insert(userDataModel);

        User user = userMapper.ToDomainModel(userDataModel);
        return user;
    }

    public async Task<User?> Get(string id)
    {
        ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Filter("id", Postgrest.Constants.Operator.Equals, id).Get();
        if(response.Models.Count == 0)
            return null;

        User r = userMapper.ToDomainModel(response.Models[0]);
        return r;
    }

    private readonly Client client;
    private readonly UserMapper userMapper = new UserMapper();
}