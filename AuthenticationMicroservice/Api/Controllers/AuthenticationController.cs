using Microsoft.AspNetCore.Mvc;

using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Services;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Domain.Models;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Responses;
using ChiraagShoeAppBackend.AuthenticationMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Models;

namespace ChiraagShoeAppBackend.AuthenticationMicroservice.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(SignUpService signUpService, LoginService loginService, UserRepository userRepository)
    {
        this.signUpService = signUpService;
        this.loginService = loginService;
        this.userRepository = userRepository;
    }

    [HttpPost("SignUp")]
    public async Task<ActionResult<AuthenticationResponse>> SignUp([FromBody] SignUpRequest request)
    {
        Console.WriteLine($"CustomLog: Request = {request}");

        User user;
        try 
        {
            user = await signUpService.SignUp(request.Username, request.Email, request.Password);
        }
        catch
        {
            return Problem(detail: "Error signing up");
        }

        await userRepository.Store(user);

        return new AuthenticationResponse(Id: user.Id, Email: request.Email, message: "Signed up successfully");
    }

    [HttpPost("LoginWithEmail")]
    public async Task<ActionResult<AuthenticationResponse>> LoginWithEmail([FromBody] LoginWithEmailRequest loginWithEmailRequest)
    {
        User user;
        try 
        {
            user = await loginService.LoginWithEmail(loginWithEmailRequest.Email, loginWithEmailRequest.Password);
        }
        catch
        {
            return Problem(detail: "Error logging in");
        }

        return new AuthenticationResponse(user.Id, Email: loginWithEmailRequest.Email, message: "Logged in successfully");
    }

    [HttpPost("LoginWithUsername")]
    public async Task<ActionResult<AuthenticationResponse>> LoginWithUsername([FromBody] LoginWithUsernameRequest request)
    {
        User user;
        try 
        {
            user = await loginService.LoginWithUsername(request.Username, request.Password);
        }
        catch
        {
            return Problem(detail: "Error logging in");
        }
        
        return new AuthenticationResponse(user.Id, Email: user.Email, message: "Logged in successfully");
    }



    private readonly SignUpService signUpService;
    private readonly LoginService loginService;
    private readonly UserRepository userRepository;
}