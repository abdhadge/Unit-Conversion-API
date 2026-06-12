using FluentAssertions;
using UnitConversion.Application.Strategies;
using UnitConversion.Domain.Enums;
using Xunit;

namespace UnitConversion.Tests.Strategies;

public class LengthConversionStrategyTests
{
    private readonly LengthConversionStrategy _strategy = new();

    [Fact]
    public void Category_ShouldBeLength()
    {
        // Arrange & Act
        var category = _strategy.Category;

        // Assert
        category.Should().Be(UnitCategory.Length);
    }

    [Fact]
    public void MeterToFoot_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 10.0;

        // Act
        var result = _strategy.Convert(value, "meter", "foot");

        // Assert
        result.Should().BeApproximately(32.8084, 0.0001);
    }

    [Fact]
    public void FootToMeter_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 100.0;

        // Act
        var result = _strategy.Convert(value, "foot", "meter");

        // Assert
        result.Should().BeApproximately(30.48, 0.0001);
    }

    [Fact]
    public void CanConvert_WithTwoLengthUnits_ShouldReturnTrue()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("meter", "kilometer");

        // Assert
        canConvert.Should().BeTrue();
    }

    [Fact]
    public void CanConvert_WithNonLengthUnit_ShouldReturnFalse()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("meter", "celsius");

        // Assert
        canConvert.Should().BeFalse();
    }
}
