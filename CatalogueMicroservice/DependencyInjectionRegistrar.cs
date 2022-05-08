namespace ChiraagShoeAppBackend.CatalogueMicroservice;

public class DependencyInjectionRegistrar
{
    public void AddDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<Domain.Services.ProductService>();
        builder.Services.AddSingleton<Api.Mappers.ProductMapper>();
        builder.Services.AddSingleton<DataAccess.Mappers.ProductMapper>();
        builder.Services.AddSingleton<Domain.Services.IProductRepository, DataAccess.Repositories.ProductRepository>();
        builder.Services.AddSingleton<DataAccess.Mappers.BidMapper>();

        builder.Services.AddSingleton<Domain.Services.IBidRepository, DataAccess.Repositories.SupabaseBidRepository>();
        builder.Services.AddSingleton<Domain.Services.BidService>();
        builder.Services.AddSingleton<Api.Mappers.BidMapper>();

        builder.Services.AddSingleton<Api.Mappers.OrderMapper>();
        builder.Services.AddSingleton<DataAccess.Mappers.OrderMapper>();
        builder.Services.AddSingleton<Domain.Services.IOrderRepository, DataAccess.Repositories.SupabaseOrderRepository>();
        builder.Services.AddSingleton<Domain.Services.OrderService>();

        builder.Services.AddSingleton<Domain.Services.IUserRepository, DataAccess.Repositories.SupabaseUserRepository>();
        builder.Services.AddSingleton<DataAccess.Mappers.UserMapper>();
        builder.Services.AddSingleton<Api.Mappers.UserMapper>();
    }
}