using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Models;

/// <summary>
/// Represents the result of a successful unit conversion.
/// </summary>
public class ConversionResponse
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
    /// The resulting converted value.
    /// </summary>
    public double ConvertedValue { get; set; }

    /// <summary>
    /// The category of measurement (e.g. Length, Weight, Temperature).
    /// </summary>
    public UnitCategory Category { get; set; }
}
