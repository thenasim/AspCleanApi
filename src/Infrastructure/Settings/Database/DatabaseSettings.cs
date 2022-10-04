namespace Infrastructure.Settings.Database;

public class DatabaseSettings
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; set; } = null;
    public string? DatabaseVersion { get; set; } = null;
    public int MaxRetryOnFailure { get; set; }
    public int? CommandTimeout { get; set; } = null;
}
