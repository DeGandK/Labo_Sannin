using Labo_DAL.Repositories;
using DAL = Labo_DAL.Services;
using BLL = Labo_BLL.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Labo_BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Labo_Sannin_API.Tools;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepo, DAL.UserService>();
builder.Services.AddScoped<IUserService, BLL.UserService>();

builder.Services.AddScoped<IProductRepo, DAL.ProductService>();
builder.Services.AddScoped<ICommandRepo, DAL.CommandService>();
builder.Services.AddScoped<ICommandService, BLL.CommandService>();
builder.Services.AddScoped<TokenGenerator>();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("TokenInfo").GetSection("secretKey").Value)),
                ValidateLifetime = true,
                ValidAudience = "https://monclient.com",
                ValidIssuer = "https://monapi.com",
                ValidateAudience = false
            };
        }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("adminPolicy", policy => policy.RequireRole("admin"));
    //options.AddPolicy("modoPolicy", policy => policy.RequireRole("admin", "moderator"));
    //options.AddPolicy("adminPolicy", policy => policy.RequireClaim("UserId", "1");
    options.AddPolicy("isConnectedPolicy", policy => policy.RequireAuthenticatedUser());
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
