using Microsoft.EntityFrameworkCore;
using Planner.IRepository;
using Planner.Models;
using Planner.Repository;
using Planner.Service;
using System.Text.Json.Serialization;

// usado para configurar os serviços e o pipeline de middleware da aplicação.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adiciona suporte para controladores e views no padrão MVC à aplicação
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Adiciona suporte para enums como strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add the configuration
var configuration = builder.Configuration;
// Define o caminho, "appsettings.json" como fonte de configuração(obrigatorio)
configuration.SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();

// Add DbContext
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlite(configuration.GetConnectionString("ConexaoSQLite")));

// Adicionar UsuarioService ao contêiner
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// Adicionar TarefaService ao contêiner
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<TarefaService>();

// Adicionar MetaService ao contêiner
builder.Services.AddScoped<IMetaRepository, MetaRepository>();
builder.Services.AddScoped<MetaService>();

// Adicionar LembreteService ao contêiner
builder.Services.AddScoped<ILembreteRepository, LembreteRepository>();
builder.Services.AddScoped<LembreteService>();

// Adicionar RelatorioService ao contêiner
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<RelatorioService>();

// Constrói a aplicação com base nas configurações feitas no builder
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//HTTP -> HTTPS
app.UseHttpsRedirection();

//Habilita o serviço de arquivos estáticos, servindo arquivos como imagens, CSS e JavaScript da pasta wwwroot
app.UseStaticFiles();


// Habilita o roteamento, que define como as solicitações HTTP são mapeadas para os controladores e ações correspondentes.
app.UseRouting();


// Habilita a autenticação e autorização
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();