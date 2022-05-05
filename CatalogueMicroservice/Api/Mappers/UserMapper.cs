using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class UserMapper
{
    public UserDto ToDto(User domainModel) => new UserDto(domainModel.Id, domainModel.Username);
}