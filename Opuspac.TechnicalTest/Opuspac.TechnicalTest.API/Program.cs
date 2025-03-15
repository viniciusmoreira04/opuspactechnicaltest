using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Npgsql;
using Opuspac.TechnicalTest.API.Database;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Application.Services;
using Opuspac.TechnicalTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.Configure<PostgreSettings>(builder.Configuration.GetSection("PostgreSettings"));

builder.Services.AddSingleton(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(MongoRepository<>));
builder.Services.AddScoped(typeof(PostgreRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderServiceRepository, OrderServiceRepository>();
builder.Services.AddScoped<IOrderService, Opuspac.TechnicalTest.Application.Services.OrderService>();
builder.Services.AddScoped<CounterService>();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddTransient<NpgsqlConnection>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<PostgreSettings>>().Value;
    return new NpgsqlConnection(settings.ConnectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapFallbackToFile("/index.html");

app.Run();