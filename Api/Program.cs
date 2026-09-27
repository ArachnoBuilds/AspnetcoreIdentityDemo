using Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<AppDbContext>("AspnetcoreIdentityDemo");

builder.Services
    .AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan =
            TimeSpan.FromMinutes(5);
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            //IssuerSigningKey = new SymmetricSecurityKey(builder.Configuration["IssuerSigningKey"]),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/sign-up", Api.Endpoint.SignUpAsync).AllowAnonymous();

app.Run();