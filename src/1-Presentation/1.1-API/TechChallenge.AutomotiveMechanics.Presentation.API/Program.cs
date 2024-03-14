using MassTransit;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using TechChallenge.AutomotiveMechanics.Crosscutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq((ctx, busConfigurator) =>
    {
        var connectionString = builder.Configuration.GetConnectionString("RabbitMq") ?? "amqp://user:password@localhost:5672";
        busConfigurator.Host(connectionString);
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{

//}
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
