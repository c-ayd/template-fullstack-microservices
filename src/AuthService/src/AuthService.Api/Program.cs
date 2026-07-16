using AuthService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//~ Begin - Register services from layers
builder.Services.AddPersistenceServices(builder.Configuration);
//~ End

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
