using Application;
using Infrastructure;
using Presentation.Controllers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services
    .AddPresentationServices()
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();