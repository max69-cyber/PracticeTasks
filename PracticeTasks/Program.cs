using Microsoft.AspNetCore.Diagnostics;
using PracticeTasks.Services;
using PracticeTasks.Services.Interfaces;
using PracticeTasks.Configuration;
using PracticeTasks.Middleware;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IStringsService,StringsService>();
builder.Services.AddSingleton<ISortingService,SortingService>();
builder.Services.AddHttpClient<IRandomService, RandomService>();
builder.Services.Configure<AppSettings>(builder.Configuration);

var configuration = builder.Configuration.Get<AppSettings>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();    //Swagger UI
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ParallelLimitMiddleware>(configuration.Settings.ParallelLimit);

app.MapControllers();

app.Run();
