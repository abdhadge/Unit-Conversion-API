using UnitConversion.Domain.Enums;

namespace UnitConversion.Domain.Interfaces;

/// <summary>
/// Defines a strategy responsible for converting values between units
/// within a single category of measurement (e.g. Length, Weight, Temperature).
/// </summary>
public interface IConversionStrategy
{
    /// <summary>
    /// The measurement category this strategy is responsible for.
    /// </summary>
    UnitCategory Category { get; }

    /// <summary>
    /// Determines whether this strategy can handle conversions between the given units.
    /// </summary>
    /// <param name="fromUnit">The source unit name.</param>
    /// <param name="toUnit">The target unit name.</param>
    /// <returns>True if both units are supported by this strategy.</returns>
    bool CanConvert(string fromUnit, string toUnit);

    /// <summary>
    /// Converts a value from one unit to another.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="fromUnit">The source unit name.</param>
    /// <param name="toUnit">The target unit name.</param>
    /// <returns>The converted value.</returns>
    double Convert(double value, string fromUnit, string toUnit);
}
