using UnitConversion.Domain.Models;

namespace UnitConversion.Application.Interfaces;

/// <summary>
/// Defines the application-level service for performing unit conversions.
/// </summary>
public interface IConversionService
{
    /// <summary>
    /// Converts a value from one unit to another, selecting the appropriate
    /// strategy based on the requested units.
    /// </summary>
    /// <param name="request">The conversion request containing value, source unit, and target unit.</param>
    /// <returns>A <see cref="ConversionResponse"/> containing the converted value and metadata.</returns>
    ConversionResponse Convert(ConversionRequest request);
}
