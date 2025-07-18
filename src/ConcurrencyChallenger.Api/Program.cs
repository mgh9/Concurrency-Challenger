using System.Net;
using ConcurrencyChallenger.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDistributedLockFactory>(_ =>
{
    var redisEndpoints = new[] { new RedLockEndPoint { EndPoint = new DnsEndPoint("localhost", 6379) } };
    return RedLockFactory.Create(redisEndpoints);
});

var app = builder.Build();

InitDatabase(app);

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

static void InitDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}