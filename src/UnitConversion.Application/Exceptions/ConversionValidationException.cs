namespace UnitConversion.Application.Exceptions;

/// <summary>
/// Thrown when a conversion request fails validation (e.g. invalid unit, unsupported conversion).
/// This exception maps to an HTTP 400 Bad Request response.
/// </summary>
public class ConversionValidationException : Exception
{
    public ConversionValidationException(string message) : base(message)
    {
    }
}
