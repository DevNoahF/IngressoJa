using IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Vendas.Application.UseCases;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Infrastructure.Persistence.Repositories;
using IngressoJa.Contexts.Eventos.Infrastructure.Persistence.DbContexts;
using IngressoJa.Data.Persistence.Repositories;
using IngressoJa.Data.Sales;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using IngressoJa.Contexts.Eventos.Application.UseCases.Event;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Vendas
builder.Services.AddDbContext<SaleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VendasConnection")));
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<CreateSaleUseCase>();
builder.Services.AddScoped<GetSaleByIdUseCase>();
builder.Services.AddScoped<UpdateSaleStatusUseCase>();

// Eventos
builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EventosConnection")));
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<CreateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<GetAllEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();

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