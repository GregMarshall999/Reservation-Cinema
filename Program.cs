using Microsoft.EntityFrameworkCore;
using ReservationCinema.Data;
using ReservationCinema.Repositories;
using ReservationCinema.Services;

var builder = WebApplication.CreateBuilder(args);



// ____________________________________________________________________________________
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 
builder.Services.AddScoped<ICinemaRepository, CinemaRepository>(); 
builder.Services.AddScoped<IFilmRepository, FilmRepository>(); 
builder.Services.AddScoped<FilmService>();
builder.Services.AddScoped<CinemaService>();
builder.Services.AddScoped<FilmService>(); 


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(4, 9, 7)) //Adapter à la version sur WAMP
    )
);


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "film",
    pattern: "film",
    defaults: new { controller = "Film", action = "Index" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
