using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelProjectAPI.Models;

public class ApiKey
{

    public int Id { get; set; }
    [MaxLength(256)]
    public string Key { get; set; } = string.Empty;

    [MaxLength(200)]
    public string AppName { get; set; } = string.Empty;

    public DateTimeOffset? ExpiresAtUtc { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    [NotMapped] // This property is not mapped to the database, it's calculated based on the ExpiresAtUtc property.
    public bool IsActive => !ExpiresAtUtc.HasValue || ExpiresAtUtc.Value > DateTimeOffset.UtcNow;
}
