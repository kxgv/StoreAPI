using StoreAPI.WebApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Resolve dependencies injection
builder.Services.AddInfrastructure(builder.Configuration);  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configurar autenticación con JWT
//var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]);


// Authentication
builder.Services.AddAuthentication();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true
//    };
//});

builder.Services.AddSwaggerGen();

var allowedFrontend = builder.Configuration.GetValue<string>("OriginAllowed")!.Split(",");

// Angular allow CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(politics =>
    {
        politics.WithOrigins(allowedFrontend).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// JSON atributes to CamelCase
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
