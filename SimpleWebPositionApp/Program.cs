using Microsoft.EntityFrameworkCore;
using SimpleWebPositionApp.Data;
using SimpleWebPositionApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerDocument();
builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    db.Database.Migrate();
    if (!db.Login.Any()||!db.Login.Where(value => value.mode == Mode.Root).Any()) {
        int index = 1;
        if (db.Login.Any()) {
            index = db.Login.Max(value => value.Login_id);
        }
        db.Login.Add(new()
        {
            Login_id = index,
            Login_Name = "root",
            Pass = "12345",
            mode= Mode.Root
        });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product68}/{action=Index}/{id?}");

app.Run();
