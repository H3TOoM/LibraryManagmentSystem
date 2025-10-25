using LibraryManagmentSystem.Infrasturcture;
using LibraryManagmentSystem.Infrasturcture.Data;
using LibraryManagmentSystem.Infrasturcture.Repoistories;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using LibraryManagmentSystem.Services;
using LibraryManagmentSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



// Register DBContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString( "DefaultConnection" ) ));


#region Dependency Injection
builder.Services.AddInfrastuctreDependcies()
                .AddServicesDependcies();
#endregion



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Scalar
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
