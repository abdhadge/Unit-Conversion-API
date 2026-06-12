using Microsoft.AspNetCore.Mvc;
using UnitConversion.Api.DTOs;
using UnitConversion.Application.Exceptions;
using UnitConversion.Application.Interfaces;
using UnitConversion.Domain.Models;

namespace UnitConversion.Api.Controllers;

/// <summary>
/// Provides endpoints for converting values between units of measurement.
/// </summary>
[ApiController]
[Route("api/conversion")]
[Produces("application/json")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionController(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    /// <summary>
    /// Converts a numeric value from one unit of measurement to another.
    /// </summary>
    /// <param name="value">The numeric value to convert.</param>
    /// <param name="from">The unit to convert from (e.g. "meter").</param>
    /// <param name="to">The unit to convert to (e.g. "foot").</param>
    /// <returns>The converted value along with metadata about the conversion.</returns>
    /// <response code="200">Returns the converted value.</response>
    /// <response code="400">The request was invalid (missing parameters, unknown unit, or incompatible units).</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<ConversionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public IActionResult Get([FromQuery] double? value, [FromQuery] string? from, [FromQuery] string? to)
    {
        if (value is null)
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Query parameter 'value' is required and must be a valid number."));
        }

        if (string.IsNullOrWhiteSpace(from))
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Query parameter 'from' is required."));
        }

        if (string.IsNullOrWhiteSpace(to))
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Query parameter 'to' is required."));
        }

        if (string.Equals(from.Trim(), to.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(ApiResponse<object>.FailureResponse("Source and target units must be different."));
        }

        var request = new ConversionRequest
        {
            Value = value.Value,
            FromUnit = from,
            ToUnit = to
        };

        try
        {
            var result = _conversionService.Convert(request);

            var responseDto = new ConversionResponseDto
            {
                OriginalValue = result.OriginalValue,
                FromUnit = result.FromUnit,
                ToUnit = result.ToUnit,
                ConvertedValue = result.ConvertedValue,
                Category = result.Category.ToString()
            };

            return Ok(ApiResponse<ConversionResponseDto>.SuccessResponse(responseDto));
        }
        catch (ConversionValidationException ex)
        {
            return BadRequest(ApiResponse<object>.FailureResponse(ex.Message));
        }
    }
}
