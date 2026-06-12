using UnitConversion.Application.Exceptions;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Application.Strategies;

/// <summary>
/// Handles conversions between weight/mass units by converting through a common base unit (grams).
/// </summary>
public class WeightConversionStrategy : IConversionStrategy
{
    public UnitCategory Category => UnitCategory.Weight;

    /// <summary>
    /// Conversion factors from each supported unit to grams (the base unit).
    /// </summary>
    private static readonly Dictionary<string, double> UnitToGramFactors = new(StringComparer.OrdinalIgnoreCase)
    {
        ["gram"] = 1.0,
        ["kilogram"] = 1000.0,
        ["pound"] = 453.59237,
        ["ounce"] = 28.349523125
    };

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return UnitToGramFactors.ContainsKey(fromUnit) && UnitToGramFactors.ContainsKey(toUnit);
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        if (!UnitToGramFactors.TryGetValue(fromUnit, out var fromFactor))
        {
            throw new UnknownUnitException(fromUnit);
        }

        if (!UnitToGramFactors.TryGetValue(toUnit, out var toFactor))
        {
            throw new UnknownUnitException(toUnit);
        }

        var valueInGrams = value * fromFactor;
        return valueInGrams / toFactor;
    }
}
