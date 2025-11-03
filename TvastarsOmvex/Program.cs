using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvexMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add Identity Authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ Add MVC Controllers and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await SeedData.SeedAdminUser(services);
//}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// ✅ Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // 👈 for /Identity pages

app.Run();
