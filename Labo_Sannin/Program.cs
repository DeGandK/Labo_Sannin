using Labo_DAL.Repositories;
using DAL = Labo_DAL.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Labo_BLL.Interfaces;
using BLL = Labo_BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepo, DAL.UserService>();
builder.Services.AddScoped<IProductRepo, DAL.ProductService>();
builder.Services.AddScoped<ICommandRepo, DAL.CommandService>();
builder.Services.AddScoped<ICommandService, BLL.CommandService>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

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

app.Run();
