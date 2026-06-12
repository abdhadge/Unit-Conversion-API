using UnitConversion.Application.Exceptions;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Application.Strategies;

/// <summary>
/// Handles conversions between length units by converting through a common base unit (meters).
/// </summary>
public class LengthConversionStrategy : IConversionStrategy
{
    public UnitCategory Category => UnitCategory.Length;

    /// <summary>
    /// Conversion factors from each supported unit to meters (the base unit).
    /// </summary>
    private static readonly Dictionary<string, double> UnitToMeterFactors = new(StringComparer.OrdinalIgnoreCase)
    {
        ["meter"] = 1.0,
        ["kilometer"] = 1000.0,
        ["centimeter"] = 0.01,
        ["inch"] = 0.0254,
        ["foot"] = 0.3048,
        ["yard"] = 0.9144,
        ["mile"] = 1609.344
    };

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return UnitToMeterFactors.ContainsKey(fromUnit) && UnitToMeterFactors.ContainsKey(toUnit);
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        if (!UnitToMeterFactors.TryGetValue(fromUnit, out var fromFactor))
        {
            throw new UnknownUnitException(fromUnit);
        }

        if (!UnitToMeterFactors.TryGetValue(toUnit, out var toFactor))
        {
            throw new UnknownUnitException(toUnit);
        }

        var valueInMeters = value * fromFactor;
        return valueInMeters / toFactor;
    }
}
