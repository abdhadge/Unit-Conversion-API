namespace UnitConversion.Application.Exceptions;

/// <summary>
/// Thrown when a requested unit is not recognized by any conversion strategy.
/// </summary>
public class UnknownUnitException : Exception
{
    public UnknownUnitException(string unit)
        : base($"Unit '{unit}' is not recognized.")
    {
    }
}
