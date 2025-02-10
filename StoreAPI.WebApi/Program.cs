using StoreAPI.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Resolve dependencies injection
builder.Services.AddInfrastructure(builder.Configuration);  

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedFrontend = builder.Configuration.GetValue<string>("OriginAllowed")!.Split(",");
// angular 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(politics =>
    {
        politics.WithOrigins(allowedFrontend).AllowAnyHeader().AllowAnyMethod();
    });
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

app.UseAuthorization();

app.MapControllers();

app.Run();
