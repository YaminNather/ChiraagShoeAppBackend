using ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;
using Postgrest.Responses;

namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Services;

public class SignUpService
{
    public SignUpService(Supabase.Client client, UserRepository userRepository)
    {
        this.client = client;
        this.userRepository = userRepository;
    }

    public async Task<User> SignUp(string username, string email, string password)
    {
        if(password.Length < 6)
            throw new InvalidPasswordFormatException();

        bool usernameExists = await checkIfUsernameExists(username);
        if(usernameExists)
            throw new UsernameAlreadyExistsException();            

        Supabase.Gotrue.Session session;
        try {
            session = await client.Auth.SignUp(email, password);
        }
        catch {
            throw new SignUpException();
        }        

        User user = new User(session.User.Id, username, session.User.Email, password);

        return user;
    }

    private async Task<bool> checkIfUsernameExists(string username)
    {
        User? user = await userRepository.GetWithUsername(username);

        return user != null;
    }


    private readonly Supabase.Client client;
    private readonly UserRepository userRepository;
}

public class SignUpException : System.Exception {}

public class UsernameAlreadyExistsException : System.Exception {}
public class InvalidPasswordFormatException : System.Exception {}