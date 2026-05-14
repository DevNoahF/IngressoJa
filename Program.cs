using IngressoJa.Contexts.Eventos.Infrastructure.Config.Jwt;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Vendas.Application.UseCases;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Vendas.Infrastructure.Persistence.DbContexts;
using IngressoJa.Contexts.Vendas.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Implementation .env to envieronment variables to appsettings.json
Env.Load();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VendasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VendasConnection")));
builder.Services.AddScoped<IVendaRepository, VendaRepository>();
builder.Services.AddScoped<RealizarVendaUseCase>();
builder.Services.AddScoped<ObterVendaUseCase>();
builder.Services.AddScoped<ProcessarPagamentoUseCase>();

// JWT auth and token generator
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<ITokenGenerate, TokenGenerate>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
