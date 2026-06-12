using UnitConversion.Api.Middleware;
using UnitConversion.Application.Interfaces;
using UnitConversion.Application.Services;
using UnitConversion.Application.Strategies;
using UnitConversion.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register conversion strategies. To support a new category, add a new
// IConversionStrategy implementation and register it here.
builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, WeightConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();

builder.Services.AddScoped<IConversionService, ConversionService>();

// Swagger / OpenAPI configuration.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Global exception handling middleware - placed first to catch all downstream exceptions.
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Exposed for integration testing via WebApplicationFactory.
public partial class Program { }
