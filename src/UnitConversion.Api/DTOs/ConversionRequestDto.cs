namespace UnitConversion.Api.DTOs;

/// <summary>
/// Represents the query parameters for a unit conversion request.
/// </summary>
public class ConversionRequestDto
{
    /// <summary>
    /// The numeric value to convert.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// The unit to convert from (e.g. "meter", "celsius", "kilogram").
    /// </summary>
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// The unit to convert to (e.g. "foot", "fahrenheit", "pound").
    /// </summary>
    public string To { get; set; } = string.Empty;
}
