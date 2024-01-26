using TrelloMVC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<DbTrelloContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularApp",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://trellolite.azurewebsites.net", "http://localhost:4200")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();