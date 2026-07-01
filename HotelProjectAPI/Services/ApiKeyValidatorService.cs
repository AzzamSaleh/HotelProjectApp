using HotelProjectAPI.Contracts;
using HotelProjectAPI.Data;

namespace HotelProjectAPI.Services;



public class ApiKeyValidatorService(IConfiguration configuration) : IApiKeyValidatorService
{

    public Task<bool> IsValidAsync(string apiKey, CancellationToken ct = default)
    {
        // inject the Db Context 

        // Validate API keys against the database
        // this implement if we hava a database of API keys, we can check if the provided API key exists in the database and is valid.
        //if (string.IsNullOrWhiteSpace(apiKey)) return false;

        //var apiKeyEntity = await db.ApiKeys
        //    .AsNoTracking()
        //    .FirstOrDefaultAsync(k => k.Key == apiKey, ct);

        //if (apiKeyEntity is null) return false;

        //// If there is no expiry date or the expiry date does not exceed today's date.
        //return apiKeyEntity.IsActive;


        return Task.FromResult(apiKey.Equals(configuration["ApiKey"]));

    }

}
