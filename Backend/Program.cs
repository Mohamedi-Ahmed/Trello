using TrelloMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter un DBContext
builder.Services.AddDbContext<DbTrelloContext>();

// Ajouter la configuration CORS pour autoriser les requêtes du front-end Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add services to the container with no Views
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activer CORS
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
