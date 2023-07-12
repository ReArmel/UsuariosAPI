using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Database;
using UsuariosAPI.Models;
using UsuariosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectString = builder.Configuration.GetConnectionString("UsuarioConnection");

builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
    });

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<UsuarioService>();

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
