using FluentAssertions;
using UnitConversion.Application.Exceptions;
using UnitConversion.Application.Services;
using UnitConversion.Application.Strategies;
using UnitConversion.Domain.Enums;
using UnitConversion.Domain.Interfaces;
using UnitConversion.Domain.Models;
using Xunit;

namespace UnitConversion.Tests.Services;

public class ConversionServiceTests
{
    private static ConversionService CreateService()
    {
        var strategies = new List<IConversionStrategy>
        {
            new LengthConversionStrategy(),
            new WeightConversionStrategy(),
            new TemperatureConversionStrategy()
        };

        return new ConversionService(strategies);
    }

    [Fact]
    public void Convert_MeterToFoot_ShouldReturnCorrectResult()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 10, FromUnit = "meter", ToUnit = "foot" };

        // Act
        var result = service.Convert(request);

        // Assert
        result.ConvertedValue.Should().BeApproximately(32.8084, 0.0001);
        result.Category.Should().Be(UnitCategory.Length);
        result.FromUnit.Should().Be("meter");
        result.ToUnit.Should().Be("foot");
    }

    [Fact]
    public void Convert_KilogramToPound_ShouldReturnCorrectResult()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 10, FromUnit = "kilogram", ToUnit = "pound" };

        // Act
        var result = service.Convert(request);

        // Assert
        result.ConvertedValue.Should().BeApproximately(22.0462, 0.0001);
        result.Category.Should().Be(UnitCategory.Weight);
    }

    [Fact]
    public void Convert_CelsiusToFahrenheit_ShouldReturnCorrectResult()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 25, FromUnit = "celsius", ToUnit = "fahrenheit" };

        // Act
        var result = service.Convert(request);

        // Assert
        result.ConvertedValue.Should().BeApproximately(77.0, 0.0001);
        result.Category.Should().Be(UnitCategory.Temperature);
    }

    [Fact]
    public void Convert_WithInvalidUnit_ShouldThrowConversionValidationException()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 10, FromUnit = "banana", ToUnit = "foot" };

        // Act
        var act = () => service.Convert(request);

        // Assert
        act.Should().Throw<ConversionValidationException>()
            .WithMessage("*banana*not recognized*");
    }

    [Fact]
    public void Convert_WithIncompatibleCategories_ShouldThrowConversionValidationException()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 10, FromUnit = "kilogram", ToUnit = "celsius" };

        // Act
        var act = () => service.Convert(request);

        // Assert
        act.Should().Throw<ConversionValidationException>()
            .WithMessage("*different unit categories*");
    }

    [Fact]
    public void Convert_WithMissingUnits_ShouldThrowConversionValidationException()
    {
        // Arrange
        var service = CreateService();
        var request = new ConversionRequest { Value = 10, FromUnit = "", ToUnit = "foot" };

        // Act
        var act = () => service.Convert(request);

        // Assert
        act.Should().Throw<ConversionValidationException>();
    }
}
