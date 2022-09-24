using Microsoft.Extensions.Options;

namespace Infrastructure.Settings.Database;

public class ValidateDatabaseSettings : IValidateOptions<DatabaseSettings>
{
    public ValidateOptionsResult Validate(string name, DatabaseSettings options)
    {
        const string errorHeadline = "Validation failed in appsettings.json";

        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail($"{errorHeadline}: {nameof(options.ConnectionString)} can not be empty.");
        }

        if (options.MaxRetryOnFailure < 0)
        {
            return ValidateOptionsResult.Fail($"{errorHeadline}: {nameof(options.MaxRetryOnFailure)} can not be less than 0.");
        }

        if (options.CommandTimeout < 30)
        {
            return ValidateOptionsResult.Fail($"{errorHeadline}: {nameof(options.CommandTimeout)} can not be less than 30.");
        }

        return ValidateOptionsResult.Success;
    }
}
