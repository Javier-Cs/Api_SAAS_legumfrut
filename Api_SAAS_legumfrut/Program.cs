using Api_SAAS_legumfrut.Data;
using Api_SAAS_legumfrut.Dtos.cliente.validador;
using Api_SAAS_legumfrut.Repository;
using Api_SAAS_legumfrut.Services;
using FluentValidation;
using MediatR;
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
builder.Services.AddValidatorsFromAssembly(typeof(ClienteCreateValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAstroApp",
        policy => 
        {
            policy.WithOrigins(
                "http://localhost:1975",
                "http://localhost:4321",
                "https://saas.legumfrutsa.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
            
        });
    }
);


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

/* Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Operaciones V1");
    c.RoutePrefix = "swagger";
});

// Redirige automáticamente peticiones HTTP a HTTPS (seguridad).
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("AllowAstroApp");

//validacion de token
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
