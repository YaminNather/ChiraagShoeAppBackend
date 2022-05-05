using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

public class UserMapper
{
    public UserDataModel FromDomainModel(User user)
    {
        return new UserDataModel()
        {
            Id = user.Id,
            Username = user.Username
        };
    }

    public User ToDomainModel(UserDataModel dataModel)
    {
        return new User(Id: dataModel.Id, Username: dataModel.Username);
    }
}