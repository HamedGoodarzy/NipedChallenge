using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NipedWebApi.Data;
using NipedWebApi.Domain;
using NipedWebApi.Domain.Validations;
using NipedWebApi.mappings;
using NipedWebApi.MiddleWares;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System.Configuration;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);


// Configure Database context

/*
    - Transient objects are always different; a new instance is provided to every controller and every service.
    - Scoped objects are the same within a request, but different across different requests.
    - Singleton objects are the same for every object and every request.
 */
var connectionString = builder.Configuration.GetSection("Niped:DatabaseConnectionString").Get<string>() ?? "";

builder.Services.AddScoped<INipedDbService, NipedDbService>();
builder.Services.AddDbContext<NipedDbContext>(db =>
{
    db.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);
// Add services to the container.


//Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Less noise from framework
    .Enrich.FromLogContext()
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.Debug()
    .WriteTo.File("c:\\Logs.txt"/*, rollingInterval:RollingInterval.Day*/)
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true,
            AutoCreateSqlDatabase = true,
        })
    .CreateLogger();

builder.Host.UseSerilog(); // Replace built-in logging


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IBaseInfoProvider, BaseInfoProvider>();
builder.Services.AddScoped<IReportProvider, ReportProvider>();
builder.Services.AddScoped<IClientProvider, ClientProvider>();
builder.Services.AddScoped<IRuleEvaluator, RuleEvaluator>();
builder.Services.AddScoped<IClientValidator, ClientValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Mappings.MappingConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
