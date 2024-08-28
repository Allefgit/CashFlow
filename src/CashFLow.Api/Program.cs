using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFLow.Api.FIlters;
using CashFLow.Api.Middleware;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Esse é um método de extensão

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFIlter)));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
