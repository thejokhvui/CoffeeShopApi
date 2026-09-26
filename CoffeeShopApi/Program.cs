using CoffeeShopApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CoffeeShopDb"));
var app = builder.Build();
app.UseMiddleware<CoffeeShopApi.Middlewares.ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();