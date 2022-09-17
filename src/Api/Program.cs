using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();

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
