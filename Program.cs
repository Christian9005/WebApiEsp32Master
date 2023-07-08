using Hangfire;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using Hangfire;
using Hangfire.SqlServer;
using WebApiEsp32Master.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ZonaDbContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));

builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("MyDbContext")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ZonaDbContext>();

    dbContext.Database.Migrate();
}

TaskDeletionScheduler.ScheduleTaskDeletion(app.Services);

app.Run();