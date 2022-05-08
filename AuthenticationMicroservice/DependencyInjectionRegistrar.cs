namespace ChiraagShoeAppBackend.AuthenticationMicroservice;

class DependencyInjectionRegistrar
{
    public void AddDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<DataAccess.Repositories.UserRepository>();
        builder.Services.AddSingleton<DataAccess.Mappers.UserMapper>();
        builder.Services.AddSingleton<Domain.Services.SignUpService>();
        builder.Services.AddSingleton<Domain.Services.LoginService>();
    }
}