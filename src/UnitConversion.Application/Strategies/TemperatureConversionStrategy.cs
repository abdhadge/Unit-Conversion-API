using UnitConversion.Application.Exceptions;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;

namespace UnitConversion.Application.Strategies;

/// <summary>
/// Handles conversions between temperature units (Celsius, Fahrenheit, Kelvin).
/// Temperature requires offset-based formulas rather than simple multiplicative factors,
/// so conversions are routed through Celsius as the base unit.
/// </summary>
public class TemperatureConversionStrategy : IConversionStrategy
{
    public UnitCategory Category => UnitCategory.Temperature;

    private static readonly HashSet<string> SupportedUnits = new(StringComparer.OrdinalIgnoreCase)
    {
        "celsius",
        "fahrenheit",
        "kelvin"
    };

    public bool CanConvert(string fromUnit, string toUnit)
    {
        return SupportedUnits.Contains(fromUnit) && SupportedUnits.Contains(toUnit);
    }

    public double Convert(double value, string fromUnit, string toUnit)
    {
        if (!SupportedUnits.Contains(fromUnit))
        {
            throw new UnknownUnitException(fromUnit);
        }

        if (!SupportedUnits.Contains(toUnit))
        {
            throw new UnknownUnitException(toUnit);
        }

        var valueInCelsius = ToCelsius(value, fromUnit);
        return FromCelsius(valueInCelsius, toUnit);
    }

    /// <summary>
    /// Converts a value from the given unit into Celsius.
    /// </summary>
    private static double ToCelsius(double value, string unit)
    {
        return unit.ToLowerInvariant() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32.0) * 5.0 / 9.0,
            "kelvin" => value - 273.15,
            _ => throw new UnknownUnitException(unit)
        };
    }

    /// <summary>
    /// Converts a value from Celsius into the given target unit.
    /// </summary>
    private static double FromCelsius(double celsiusValue, string unit)
    {
        return unit.ToLowerInvariant() switch
        {
            "celsius" => celsiusValue,
            "fahrenheit" => celsiusValue * 9.0 / 5.0 + 32.0,
            "kelvin" => celsiusValue + 273.15,
            _ => throw new UnknownUnitException(unit)
        };
    }
}
