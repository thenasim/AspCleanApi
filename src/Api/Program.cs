using Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi().AddInfrastructure(builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
