using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls();
builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(builder.Environment.IsDevelopment());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseExceptionHandler("/error");
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
