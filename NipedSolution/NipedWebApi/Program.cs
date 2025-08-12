using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NipedWebApi.Data;
using NipedWebApi.Domain;
using NipedWebApi.mappings;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);


// Configure Database context

/*
    - Transient objects are always different; a new instance is provided to every controller and every service.
    - Scoped objects are the same within a request, but different across different requests.
    - Singleton objects are the same for every object and every request.
 */

builder.Services.AddScoped<INipedDbService, NipedDbService>();
builder.Services.AddDbContext<NipedDbContext>(db =>
{
    db.UseSqlServer(builder.Configuration.GetSection("Niped:DatabaseConnectionString").Get<string>() ?? "");
}, ServiceLifetime.Scoped);
// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IBaseInfoProvider, BaseInfoProvider>();
builder.Services.AddScoped<IReportProvider, ReportProvider>();
builder.Services.AddScoped<IClientProvider, ClientProvider>();
builder.Services.AddScoped<IRuleEvaluator, RuleEvaluator>();


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

app.Run();
