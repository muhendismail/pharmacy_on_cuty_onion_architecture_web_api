using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PharmacyOnDuty.Aplication.Interfaces.Repository;
using PharmacyOnDuty.Middlewares;
using PharmacyOnDuty.Persistence.Context;
using PharmacyOnDuty.Persistence.Repositories;
using Serilog;


using Serilog.Events;
using Serilog.Settings.Configuration;
using StackExchange.Redis;
using PharmacyOnDuty.Application;
using PharmacyOnDuty.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistanceRegistration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<Logging>();
app.UseMiddleware<ErrorHandling>();

app.MapControllers();
app.Run();
