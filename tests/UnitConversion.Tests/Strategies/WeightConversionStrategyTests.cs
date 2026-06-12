using FluentAssertions;
using UnitConversion.Application.Strategies;
using UnitConversion.Domain.Enums;
using Xunit;

namespace UnitConversion.Tests.Strategies;

public class WeightConversionStrategyTests
{
    private readonly WeightConversionStrategy _strategy = new();

    [Fact]
    public void Category_ShouldBeWeight()
    {
        // Arrange & Act
        var category = _strategy.Category;

        // Assert
        category.Should().Be(UnitCategory.Weight);
    }

    [Fact]
    public void KilogramToPound_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 10.0;

        // Act
        var result = _strategy.Convert(value, "kilogram", "pound");

        // Assert
        result.Should().BeApproximately(22.0462, 0.0001);
    }

    [Fact]
    public void PoundToKilogram_ShouldConvertCorrectly()
    {
        // Arrange
        var value = 50.0;

        // Act
        var result = _strategy.Convert(value, "pound", "kilogram");

        // Assert
        result.Should().BeApproximately(22.6796, 0.0001);
    }

    [Fact]
    public void CanConvert_WithTwoWeightUnits_ShouldReturnTrue()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("gram", "ounce");

        // Assert
        canConvert.Should().BeTrue();
    }

    [Fact]
    public void CanConvert_WithNonWeightUnit_ShouldReturnFalse()
    {
        // Arrange & Act
        var canConvert = _strategy.CanConvert("kilogram", "meter");

        // Assert
        canConvert.Should().BeFalse();
    }
}
