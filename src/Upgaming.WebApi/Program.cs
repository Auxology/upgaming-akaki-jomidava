using Upgaming.Application;
using Upgaming.Infrastructure;
using Upgaming.WebApi.Endpoints;
using Upgaming.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();


app.UseHttpsRedirection();

app.MapBookEndpoints();
app.MapAuthorEndpoints();

await app.RunAsync();
