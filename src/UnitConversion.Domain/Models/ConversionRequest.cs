namespace UnitConversion.Domain.Models;

/// <summary>
/// Represents a request to convert a value from one unit to another.
/// </summary>
public class ConversionRequest
{
    /// <summary>
    /// The numeric value to convert.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// The unit to convert from (e.g. "meter").
    /// </summary>
    public string FromUnit { get; set; } = string.Empty;

    /// <summary>
    /// The unit to convert to (e.g. "foot").
    /// </summary>
    public string ToUnit { get; set; } = string.Empty;
}
