using FransfordSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FransfordSystem.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//Llenar base


var contextOptions = new DbContextOptionsBuilder<FransforDbContext>()
    .UseSqlServer("Server = localhost; Database = FransDb; Trusted_Connection = True;")
    .Options;
using var context = new FransforDbContext(contextOptions);

DbInit.Iniz(context);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FransforDbContext>(options =>
    options.UseSqlServer(connString));;

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()   
    .AddEntityFrameworkStores<FransforDbContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMvc();




//Para hacer la conexion con la base que se encuentra la direccion en appsettings.json




builder.Services.AddDbContext<FransforDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//{
 //   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});





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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(
    
    
    );




app.Run();
