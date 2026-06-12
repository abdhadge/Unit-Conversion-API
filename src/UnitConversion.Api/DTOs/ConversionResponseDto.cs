namespace UnitConversion.Api.DTOs;

/// <summary>
/// Represents the data payload returned for a successful conversion.
/// </summary>
public class ConversionResponseDto
{
    /// <summary>
    /// The original value supplied by the caller.
    /// </summary>
    public double OriginalValue { get; set; }

    /// <summary>
    /// The unit the value was converted from.
    /// </summary>
    public string FromUnit { get; set; } = string.Empty;

    /// <summary>
    /// The unit the value was converted to.
    /// </summary>
    public string ToUnit { get; set; } = string.Empty;

    /// <summary>
    /// The resulting converted value, rounded to 6 decimal places.
    /// </summary>
    public double ConvertedValue { get; set; }

    /// <summary>
    /// The category of measurement (e.g. "Length", "Weight", "Temperature").
    /// </summary>
    public string Category { get; set; } = string.Empty;
}

/// <summary>
/// A generic API response wrapper providing a consistent JSON envelope
/// for both successful and failed responses.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The data payload, present only on success.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// A human-readable message, typically present on failure.
    /// </summary>
    public string? Message { get; set; }

    public static ApiResponse<T> SuccessResponse(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static ApiResponse<T> FailureResponse(string message) => new()
    {
        Success = false,
        Message = message
    };
}
