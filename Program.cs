using Microsoft.EntityFrameworkCore;
using WebApplication1.data;
using WebApplication1.Interfaces;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add Automapper service
builder.Services.AddAutoMapper(typeof(Program));
//Add Swagger service
builder.Services.AddSwaggerGen();
//Add Controller Services
builder.Services.AddControllers();
//Add interface to class scopped
builder.Services.AddScoped<IProductService, ProductService>();
//Add Database services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
