using UnitConversion.Application.Exceptions;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Domain.Models;

namespace UnitConversion.Application.Services;

/// <summary>
/// Coordinates unit conversion by delegating to the appropriate <see cref="IConversionStrategy"/>.
/// New conversion categories can be supported by registering additional strategies
/// via dependency injection, without modifying this class (Open/Closed Principle).
/// </summary>
public class ConversionService : IConversionService
{
    private readonly IEnumerable<IConversionStrategy> _strategies;

    public ConversionService(IEnumerable<IConversionStrategy> strategies)
    {
        _strategies = strategies;
    }

    public ConversionResponse Convert(ConversionRequest request)
    {
        var fromUnit = request.FromUnit.Trim().ToLowerInvariant();
        var toUnit = request.ToUnit.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(fromUnit) || string.IsNullOrWhiteSpace(toUnit))
        {
            throw new ConversionValidationException("Both 'from' and 'to' units must be provided.");
        }

        var strategy = _strategies.FirstOrDefault(s => s.CanConvert(fromUnit, toUnit));

        if (strategy is null)
        {
            // Determine whether the issue is an unknown unit or an incompatible category.
            var fromKnown = _strategies.Any(s => s.CanConvert(fromUnit, fromUnit));
            var toKnown = _strategies.Any(s => s.CanConvert(toUnit, toUnit));

            if (!fromKnown)
            {
                throw new ConversionValidationException($"Unit '{request.FromUnit}' is not recognized.");
            }

            if (!toKnown)
            {
                throw new ConversionValidationException($"Unit '{request.ToUnit}' is not recognized.");
            }

            throw new ConversionValidationException(
                $"Cannot convert from '{request.FromUnit}' to '{request.ToUnit}' because they belong to different unit categories.");
        }

        var convertedValue = strategy.Convert(request.Value, fromUnit, toUnit);

        return new ConversionResponse
        {
            OriginalValue = request.Value,
            FromUnit = fromUnit,
            ToUnit = toUnit,
            ConvertedValue = Math.Round(convertedValue, 6),
            Category = strategy.Category
        };
    }
}
