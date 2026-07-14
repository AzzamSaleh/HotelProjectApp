namespace HotelProject.Api.Common.Constants;


// in this class we can define default values
// for authentication-related settings,
// such as token expiration time, issuer, audience, etc.
// This can help centralize the configuration and
// make it easier to manage authentication settings across the application.

public class AuthenticationDefaults
{

    public const string BasicScheme = "Basic";
    public const string AppName = "HotelListingApi";

    public const string ApiKeyScheme = "ApiKey";
    public const string ApiKeyHeaderName = "X-Api-Key";


}
