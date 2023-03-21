using Microsoft.EntityFrameworkCore;
using mvc1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IRepository, ProdutoRepository>();

var host = builder.Configuration["DBHOST"] ?? "localhost"; //pega as variavel de ambiente dbhost
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["DBPASSWORD"] ?? "dragao123";

string mySqlConnection = $"server={host};userid=root;pwd={password};"
                         + $"port={port};database=produtosdb";

builder.Services.AddDbContext<AppDbContext>(options =>
                              options.UseMySql(mySqlConnection,
                              ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Comentado para gerar os dados do script na pasta Mysql_Init para o container
// Populadb.IncluiDadosDB(app);

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
