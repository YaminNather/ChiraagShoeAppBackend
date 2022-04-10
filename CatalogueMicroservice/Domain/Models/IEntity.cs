namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public interface IEntity<T>
{
    bool IsSame(T other);
}