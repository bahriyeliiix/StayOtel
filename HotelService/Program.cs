﻿using HotelService.API.Extansions;
using HotelService.Extensions;
using HotelService.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Extansions
builder.Services.AddCustomServices();
builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
#endregion
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton(provider => provider.GetRequiredService<IOptions<RabbitMqSettings>>().Value);



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
