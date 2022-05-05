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

        bool emailExists = await checkIfEmailExists(email);
        if(emailExists)
            throw new EmailAlreadyExistsException();

        User r = await userRepository.Create(username, email, password);

        return r;
    }

    private async Task<bool> checkIfUsernameExists(string username)
    {
        User? user = await userRepository.GetWithUsername(username);

        return user != null;
    }

    private async Task<bool> checkIfEmailExists(string email)
    {
        User? user = await userRepository.GetWithEmail(email);

        return user != null;
    }


    private readonly Supabase.Client client;
    private readonly UserRepository userRepository;
}

public class SignUpException : System.Exception {}

public class UsernameAlreadyExistsException : System.Exception {}
public class EmailAlreadyExistsException : System.Exception {}
public class InvalidPasswordFormatException : System.Exception {}