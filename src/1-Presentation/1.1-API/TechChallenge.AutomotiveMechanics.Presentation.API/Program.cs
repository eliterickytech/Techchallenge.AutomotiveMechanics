using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using TechChallenge.AutomotiveMechanics.Crosscutting.Ioc;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutomotiveMechanics")
    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.")));

builder
    .Services.AddControllers() 
    .AddNewtonsoftJson()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplicationConfiguration(builder.Configuration);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "TechChallenge - Automotive Mechanics - Plataforma de Gestão para Mecânica 100% Digital";
    c.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
