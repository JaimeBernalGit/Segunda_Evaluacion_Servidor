using CursosAPI.Repositories;
using CursosAPI.Services;
using Models;
using CursosAPI.Repositories;
using CursosAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GestionCursosDB");

//Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IResenaRepository, ResenaRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();


//Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IResenaService, ResenaService>();
builder.Services.AddScoped<IPagoService, PagoService>();

// Add services to the container.

builder.Services.AddControllers();
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
