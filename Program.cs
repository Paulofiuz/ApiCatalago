using System.Text.Json.Serialization;
using Catalago.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

string sqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Configurando o DbContext com o SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(sqlServerConnection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();//Redireciona todas as requisições HTTP para HTTPS para garantir segurança.
app.UseAuthorization();
app.MapControllers();
app.Run();

