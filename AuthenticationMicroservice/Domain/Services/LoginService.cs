using ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;
using Postgrest.Responses;

namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Services;

public class LoginService
{
    public LoginService(Supabase.Client client, UserRepository userRepository)
    {
        this.client = client;
        this.userRepository = userRepository;
    }

    public async Task<User> LoginWithUsername(string username, string password)
    {
        User? user = await userRepository.GetWithUsername(username);

        if(user == null)
            throw new UserWithUsernameDoesntExistException();

        if(password != user.Password)
            throw new LoginException();

        return user;
    }

    public async Task<User> LoginWithEmail(string email, string password)
    {
        User? user = await userRepository.GetWithEmail(email);

        if(user == null)
            throw new UserWithEmailDoesntExistException();

        if(password != user.Password)
            throw new LoginException();

        return user;
    }



    private readonly Supabase.Client client;
    private UserRepository userRepository;
}

public class LoginException : System.Exception {}

public class UserWithUsernameDoesntExistException : System.Exception {}
public class UserWithEmailDoesntExistException : System.Exception {}