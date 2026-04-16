using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔥 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        "server=localhost;database=ApiEcommerce;user=root;password=senai",
        ServerVersion.AutoDetect("server=localhost;database=ApiEcommerce;user=root;password=senai")
    )
);

// 🔥 Controllers
builder.Services.AddControllers();

// 🔥 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();