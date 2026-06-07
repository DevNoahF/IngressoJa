using IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Application.UseCases.User;
using IngressoJa.Contexts.Eventos.Application.UseCases;
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
using IngressoJa.Contexts.Eventos.Application.Interfaces.Event;
using IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;
using IngressoJa.Contexts.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
Env.Load();

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Get environment variables
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "ingressoja";
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "ingressoja";
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
    options.UseMySql(connectionString, serverVersion, mysqlOptions => mysqlOptions.EnableRetryOnFailure()));

// Update JWT settings in configuration
builder.Configuration["JwtSettings:SecretKey"] = jwtSecret;

// Sales
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IEventSaleRepository, EventSaleRepository>();
builder.Services.AddScoped<CreateSaleUseCase>();
builder.Services.AddScoped<GetAllSalesUseCase>();
builder.Services.AddScoped<GetSaleByIdUseCase>();
builder.Services.AddScoped<GetSaleByEventUseCase>();
builder.Services.AddScoped<GetEventSalesSummaryUseCase>();
builder.Services.AddScoped<UpdateSaleStatusUseCase>();
builder.Services.AddScoped<GetByUserIdUseCase>();

// Eventos
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<CreateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();
builder.Services.AddScoped<GetAllEventsUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();

// Events in sales
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<AddEventSaleUseCase>();
builder.Services.AddScoped<DeleteEventSaleUseCase>();
builder.Services.AddScoped<GetAllEventSalesUseCase>();
builder.Services.AddScoped<GetEventSaleByIdUseCase>();
builder.Services.AddScoped<GetEventsByOrganizerIdUseCase>();
builder.Services.AddScoped<UpdateEventSaleUseCase>();

//Tickets
builder.Services.AddScoped<CreateTicketUseCase>();
builder.Services.AddScoped<GetAllTicketsUseCase>();
builder.Services.AddScoped<GetTicketByIdUseCase>();
builder.Services.AddScoped<GetTicketByUserIdUseCase>();

// User 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<IRegisterOrganizerUseCase, RegisterOrganizerUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IGetUserByEmailUseCase, GetUserByEmailUseCase>();
builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
builder.Services.AddScoped<BackEnd.Contexts.Eventos.Adapters.Interfaces.User.IGetUsersUseCase, BackEnd.Contexts.Eventos.Domain.UseCases.User.GetUsersUseCase>();
builder.Services.AddScoped<BackEnd.Contexts.Eventos.Adapters.Interfaces.User.IGetOrganizersUseCase, BackEnd.Contexts.Eventos.Domain.UseCases.User.GetOrganizerUseCase>();
builder.Services.AddScoped<BackEnd.Contexts.Eventos.Adapters.Interfaces.User.IUpdateUseCase, BackEnd.Contexts.Eventos.Domain.UseCases.User.UpdateUseCase>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

// User Sale
builder.Services.AddScoped<IUserSaleRepository, UserSaleRepository>();
builder.Services.AddScoped<IUserSaleMapper, UserSaleMapper>();
builder.Services.AddScoped<CreateUserSaleUseCase>();
builder.Services.AddScoped<GetAllUserSaleUseCase>();
builder.Services.AddScoped<GetUserSaleByIdUseCase>();
builder.Services.AddScoped<UpdateUserSaleUseCase>();
builder.Services.AddScoped<DeleteUserSaleUseCase>();

// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<ITokenGenerate, TokenGenerate>();

//config cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

// ultima verao mysql
var sqlServerVersion = new MySqlServerVersion(new Version(8, 0, 32));

builder.Services.AddDbContext<IngressoJaContext>(options =>
    options.UseMySql(connectionString, serverVersion,
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)
    )
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Apply migrations automatically - TODO: VER COMO FUNCIONA DE FATO
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IngressoJaContext>();
    db.Database.Migrate();
}

app.Run();
