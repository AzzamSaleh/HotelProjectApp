using HotelProjectAPI.Contracts;
using HotelProjectAPI.DTOs.Auth;

using HotelProjectAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HotelProjectAPI.Handlers;

// handler for basic authentication logic,
// which can be used to authenticate users based on their username and password.
public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
   IUsersService usersService
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{

    // The HandleAuthenticateAsync method is responsible for handling the authentication logic for the Basic Authentication scheme.
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {



        // checks if the Authorization header is present in the request. If it is not present, it returns an AuthenticateResult indicating that no authentication result is available.
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        // checks if the Authorization header value is present and starts with "Basic ". If it does not meet these conditions, it returns an AuthenticateResult indicating that no authentication result is available.
        // Basic Base64({username:password})
        var authHeader = authHeaderValues.ToString();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.NoResult();
        }

        // The code extracts the Base64-encoded token from the Authorization header by removing the "Basic " prefix and trimming any whitespace.
        var token = authHeader["Basic ".Length..].Trim();// tack after "Basic " and trim any whitespace
        string decoded;

        // The code attempts to decode the Base64-encoded token into a UTF-8 string. If the decoding fails (e.g., if the token is not a valid Base64 string), it returns an AuthenticateResult indicating that the Basic authentication token is invalid.
        try
        {
            var credentialBytes = Convert.FromBase64String(token);
            // {username:password}
            decoded = Encoding.UTF8.GetString(credentialBytes);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Basic authentication token.");
        }


        // The code looks for the index of the colon (':') character in the decoded string, which separates the username/email from the password. If the colon is not found or is at the beginning of the string, it returns an AuthenticateResult indicating that the Basic authentication credentials format is invalid.
        var separatorIndex = decoded.IndexOf(':');
        if (separatorIndex <= 0)
        {
            return AuthenticateResult.Fail("Invalid Basic authentication credentials format.");
        }

        //  The code extracts the username/email and password from the decoded string using the separator index. It then creates a LoginUserDto object with the extracted email and password.
        var userNameOrEmail = decoded[..separatorIndex];
        var password = decoded[(separatorIndex + 1)..];

        // The code creates a LoginUserDto object with the extracted email and password. This DTO is used to pass the login credentials to the usersService for authentication.
        var loginDto = new LoginUserDto
        {
            Email = userNameOrEmail,
            Password = password
        };


        // The code calls the LoginAsync method of the usersService to authenticate the user with the provided email and password. If the authentication fails, it returns an AuthenticateResult indicating that the username or password is invalid.
        var result = await usersService.LoginAsync(loginDto);
        if (!result.IsSuccess)
        {
            return AuthenticateResult.Fail("Invalid username or password.");
        }

        // The code creates a list of claims for the authenticated user, including a claim for the user's name (email). It then creates a ClaimsIdentity and a ClaimsPrincipal based on the claims. Finally, it creates an AuthenticationTicket with the principal and returns an AuthenticateResult indicating successful authentication.
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userNameOrEmail),
        };
        // You can add more claims here if needed, such as roles or other user information.
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        // The code returns an AuthenticateResult indicating successful authentication, along with the created AuthenticationTicket.
        return AuthenticateResult.Success(ticket);
    }















}

