using FluentAssertions;
using UnitConversion.Application.Strategies;
using UnitConversion.Domain.Enums;
using Xunit;

namespace UnitConversion.Tests.Strategies;

public class TemperatureConversionStrategyTests
{
    private readonly TemperatureConversionStrategy _strategy = new();

    [Fact]
    public void Category_ShouldBeTemperature()
    {
        // Arrange & Act
        var category = _strategy.Category;

        // Assert
        category.Should().Be(UnitCategory.Temperature);
    }

    [Fact]
    public void CelsiusToFahrenheit_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 25.0;

        // Act
        var result = _strategy.Convert(value, "celsius", "fahrenheit");

        // Assert
        result.Should().BeApproximately(77.0, 0.0001);
    }

    [Fact]
    public void FahrenheitToCelsius_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 100.0;

        // Act
        var result = _strategy.Convert(value, "fahrenheit", "celsius");

        // Assert
        result.Should().BeApproximately(37.7778, 0.0001);
    }

    [Fact]
    public void CelsiusToKelvin_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 0.0;

        // Act
        var result = _strategy.Convert(value, "celsius", "kelvin");

        // Assert
        result.Should().BeApproximately(273.15, 0.0001);
    }

    [Fact]
    public void CanConvert_WithTwoTemperatureUnits_ShouldReturnTrue()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("celsius", "kelvin");

        // Assert
        canConvert.Should().BeTrue();
    }

    [Fact]
    public void CanConvert_WithNonTemperatureUnit_ShouldReturnFalse()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("celsius", "kilogram");

        // Assert
        canConvert.Should().BeFalse();
    }
}
