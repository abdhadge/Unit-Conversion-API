using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using UnitConversion.Api.DTOs;
using Xunit;

namespace UnitConversion.Tests.Controllers;

public class ConversionControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ConversionControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ValidLengthConversion_ShouldReturnOkWithConvertedValue()
    {
        // Arrange
        var url = "/api/conversion?value=10&from=meter&to=foot";

        // Act
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<ConversionResponseDto>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        body!.Success.Should().BeTrue();
        body.Data!.ConvertedValue.Should().BeApproximately(32.8084, 0.0001);
        body.Data.Category.Should().Be("Length");
    }

    [Fact]
    public async Task Get_MissingParameters_ShouldReturnBadRequest()
    {
        // Arrange
        var url = "/api/conversion?from=meter&to=foot";

        // Act
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Get_InvalidUnit_ShouldReturnBadRequest()
    {
        // Arrange
        var url = "/api/conversion?value=10&from=banana&to=foot";

        // Act
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Success.Should().BeFalse();
        body.Message.Should().Contain("banana");
    }

    [Fact]
    public async Task Get_UnsupportedConversion_ShouldReturnBadRequest()
    {
        // Arrange
        var url = "/api/conversion?value=10&from=kilogram&to=celsius";

        // Act
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Success.Should().BeFalse();
        body.Message.Should().Contain("different unit categories");
    }

    [Fact]
    public async Task Get_SameUnitConversion_ShouldReturnBadRequest()
    {
        // Arrange
        var url = "/api/conversion?value=10&from=meter&to=meter";

        // Act
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        body!.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Get_InvalidNumericValue_ShouldReturnBadRequest()
    {
        // Arrange
        var url = "/api/conversion?value=notanumber&from=meter&to=foot";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
