using Supabase;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;

public class SupabaseUserRepository
{
    public SupabaseUserRepository(Client client, UserMapper userMapper)
    {
        this.client = client;
        this.userMapper = userMapper;
    }

    public async Task<User> Create(String username, String email, String password)
    {
        UserDataModel userDataModel = new UserDataModel{
            Username = username
        };
        ModeledResponse<UserDataModel> response = await client.From<UserDataModel>().Insert(userDataModel);

        User user = userMapper.ToDomainModel(userDataModel);
        return user;
    }


    private readonly Client client;
    private readonly UserMapper userMapper = new UserMapper();
}