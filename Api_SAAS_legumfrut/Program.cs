using Api_SAAS_legumfrut.Data;
using Api_SAAS_legumfrut.Repository;
using Api_SAAS_legumfrut.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<AuthService>();


var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// implemntacion de FluentValidation para las clase que usen IValidation<T>
// Esto permite que tus DTOs se validen automáticamente cuando llegan al controlador.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// JWT activamoe el sistema de autenticador
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // indica que se usara
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

// Habilita el sistema de roles y políticas. Esto te permite usar [Authorize] en controladores y restringir acceso según roles o claims.
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige automáticamente peticiones HTTP a HTTPS (seguridad).
app.UseHttpsRedirection();

//validacion de token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
