using ClienteProjeto.Application.Services.Relatorio;
using ClienteProjeto.CrossCutting.IoC;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Relatorio.Dto.Lancamento;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructureAPI(builder.Configuration);

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

//configuracao para injecao dependencia para servico de relatorio 
builder.Services.AddHttpClient<ChamadaRelatorioLancamentoService>();
builder.Services.AddHttpClient<ChamadaRelatorioCategoriaService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var secretKey = builder.Configuration["JWT:SecretKey"]
                   ?? throw new ArgumentException("Invalid secret key!!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    options.AddPolicy("SuperAdminOnly", policy =>
                     policy.RequireRole("Admin").RequireClaim("id", "luis"));

    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

    options.AddPolicy("ExcluisivePolicyOnly", policy =>
    
    policy.RequireAssertion(context => context.User.HasClaim(Claim =>
                            Claim.Type == "id" && Claim.Value == "luis") 
                            || context.User.IsInRole("SuperAdmin")));
});

builder.Services.AddEndpointsApiExplorer();

// 🔹 Ativar logs detalhados logo no início
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.WebHost.CaptureStartupErrors(true);
builder.WebHost.UseSetting("detailedErrors", "true");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoServico", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

//System.Text.Json
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
{
    options.AddPolicy("clienteProdutoApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("clienteProdutoApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
