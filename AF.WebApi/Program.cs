using System.Reflection;
using System.Text;
using AF.Core;
using AF.Core.Common;
using AF.Core.Settings;
using AF.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var jwtConfig = builder.Configuration.GetSection(JwtConfiguration.SectionName);


builder.Services.AddCore();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig["Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.Configure<JwtConfiguration>(jwtConfig);
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(_ => new RequestContext());
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.UseEntityFramework(o => { o.UseSqlServer(connectionString); });

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpManager();

app.Run();