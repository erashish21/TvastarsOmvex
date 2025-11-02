using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvexMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ❌ Removed Identity Authentication & Authorization middleware
// app.UseAuthentication();
// app.UseAuthorization();

// ✅ Run seeding logic (no await needed)
using (var scope = app.Services.CreateScope())
{
    SeedData.Initialize(scope.ServiceProvider);
}

// ✅ Define Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Run Application
app.Run();
