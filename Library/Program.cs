using Library.App_Data;
using Library.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<LibraryDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}
)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>();


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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//await InitalizeRoles(app.Services);

app.MapRazorPages();

app.Run();


//async Task InitalizeRoles(IServiceProvider serviceProvider)
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//        var roles = new[] { "Admin", "User" };

//        foreach (var role in roles)
//        {
//            if (!await roleManager.RoleExistsAsync(role))
//            {
//                await roleManager.CreateAsync(new IdentityRole(role));
//            }
//        }
//    }
//}


