using ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Mappers;

class UserMapper 
{
    public UserDataModel FromDomainModel(User user)
    {
        return new UserDataModel()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }

    public User ToDomainModel(UserDataModel dataModel)
    {
        return new User(
            Id: dataModel.Id,
            Username: dataModel.Username,
            Email: dataModel.Email,
            Password: dataModel.Password
        );
    }
}