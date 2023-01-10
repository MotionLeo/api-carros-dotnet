using api.Models;
using api.ModelViews;
using api.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Builder Clientes
// builder.Services.AddScoped<IServico, ClienteRepositorio>();
// builder.Services.AddScoped<IServico, ClienteRepositorioMySql>();
builder.Services.AddScoped<IServico, ClienteRepositorioEntity>();

//Builder Marcas
//builder.Services.AddScoped<IServicoMarca, MarcaRepositorio>();
builder.Services.AddScoped<IServicoMarca, MarcaRepositorioMySql>();

//Builder Configurações
//builder.Services.AddScoped<IServicoConfiguracao, ConfiguracaoRepositorio>();
builder.Services.AddScoped<IServicoConfiguracao, ConfiguracaoRepositorioMySql>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
