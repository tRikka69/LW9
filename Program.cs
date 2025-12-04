using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AmateurTheaterMongo.Validators;
using AmateurTheaterMongo.Repositories;
using AmateurTheaterMongo.Services;
using AmateurTheaterMongo.Helpers;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var secretKey = jwtSettings["SecretKey"];
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
    !string.IsNullOrEmpty(secretKey) ? secretKey : "DEFAULT_SECRET_KEY_VERY_LONG_FOR_BUILD"
));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "AmateurTheaterAPI",
        ValidAudience = jwtSettings["Audience"] ?? "AmateurTheaterClient",
        IssuerSigningKey = signingKey
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amateur Theater API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", Type = SecuritySchemeType.Http, Scheme = "bearer", BearerFormat = "JWT", In = ParameterLocation.Header, Description = "JWT Token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

builder.Services.AddValidatorsFromAssemblyContaining<PlayValidator>();
builder.Services.AddSingleton<PasswordHasher>();
builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITheaterRepository, TheaterRepository>();
builder.Services.AddScoped<IPlayRepository, PlayRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITheaterService, TheaterService>();
builder.Services.AddScoped<IPlayService, PlayService>();
builder.Services.AddScoped<IActorService, ActorService>();

var app = builder.Build();

// === ВИМОГА: Swagger у Production ===
app.UseSwagger();
app.UseSwaggerUI();
// ===================================

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();