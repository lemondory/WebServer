using WebServer.Infrastructure.CustomExceptionFilter;
using WebServer.Infrastructure.LogEntries;
using Microsoft.OpenApi.Models;
using WebServer.Application;
using WebServer.Infrastructure;
using WebServer.Persistance;
using Serilog;
using WebServer.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

// Serilog 설정 (파일 + Seq + 콘솔 등)
SerilogConfigurator.CreateLogger(builder.Configuration);
builder.Host.UseSerilog();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.Enrich.FromLogContext()
      .Enrich.WithThreadId()
      .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 1_000_000_000, // 1GB
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: 30,
        shared: true,
        flushToDiskInterval: TimeSpan.FromSeconds(2))
      .WriteTo.Console()
      .WriteTo.Seq("http://localhost:5341");

});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(LogFilter));
    config.Filters.Add(typeof(ExceptionFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "API Swagger",
        Description = "Api Swagger Documentation",
        TermsOfService = new Uri("http://swagger.io/terms/"),
        Contact = new OpenApiContact
        {
            Name = "Mustafa Ozkan"

        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "http://localhost").AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

// We register global exception handler from Infrastructure layer.
app.UseGlobalExceptionHandler();

app.MapControllers();

app.Run();
