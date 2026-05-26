using IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Application.UseCases.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Sales.Application.UseCases.Sale;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Infrastructure.Persistence.Repositories;
using IngressoJa.Data.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using DotNetEnv;
using IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Sales.Application.UseCases.EventSale;
using IngressoJa.Data.dbContext;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
Env.Load();

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Get environment variables
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "IngressoJa";
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? "dev-secret-key-change-before-production-123456";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// CORREÇÃO AQUI: Configurando o Swagger para usar o nome completo dos tipos e evitar conflitos de nomes iguais
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

// Data configuration with MySql
var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddDbContext<IngressoJaContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Update JWT settings in configuration
builder.Configuration["JwtSettings:SecretKey"] = jwtSecret;

// Sales
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IEventSaleRepository, EventSaleRepository>();
builder.Services.AddScoped<CreateSaleUseCase>();
builder.Services.AddScoped<GetSaleByIdUseCase>();
builder.Services.AddScoped<UpdateSaleStatusUseCase>();

// Eventos
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<CreateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();
builder.Services.AddScoped<GetAllEventsUseCase>();

// Events in sales
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<AddEventSaleUseCase>();
builder.Services.AddScoped<DeleteEventSaleUseCase>();
builder.Services.AddScoped<GetAllEventSalesUseCase>();
builder.Services.AddScoped<GetEventSaleByIdUseCase>();
builder.Services.AddScoped<GetEventsByOrganizerIdUseCase>();
builder.Services.AddScoped<IngressoJa.Contexts.Sales.Application.UseCases.EventSale.UpdateEventUseCase>();

// User 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<IRegisterOrganizerUseCase, RegisterOrganizerUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddScoped<IGetUserByEmailUseCase, GetUserByEmailUseCase>();
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<ITokenGenerate, TokenGenerate>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();