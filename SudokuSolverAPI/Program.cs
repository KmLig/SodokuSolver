using Microsoft.EntityFrameworkCore;
using SudokuSolverAPI.Data;
using SudokuSolverAPI.Repositories;
using SudokuSolverAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure EF Core and connection string
builder.Services.AddDbContext<SudokuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services and Repositories
builder.Services.AddScoped<ISudokuRepository, SudokuRepository>();
builder.Services.AddScoped<ISudokuService, SudokuService>();

builder.Services.AddControllers();

// Add CORS services and configure policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174") 
                          .AllowAnyMethod() 
                          .AllowAnyHeader()); 
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
