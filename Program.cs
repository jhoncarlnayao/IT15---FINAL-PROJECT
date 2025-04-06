using IT15_FINALPROJECT.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder; // Optional, for clarity

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Add Session support
builder.Services.AddSession();

// ✅ Make TempData use Session (optional but recommended)
builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.SessionStateTempDataProvider>();

builder.Services.AddDbContext<TenantContext>(options =>
{
    options.UseMySql("server=127.0.0.1;database=IT15FINAL;user=root;password=;",
        new MySqlServerVersion(new Version(10, 4, 32))
    );
});

builder.Services.AddDbContext<AdminContext>(options =>
{
    options.UseMySql("server=127.0.0.1;database=IT15FINAL;user=root;password=;",
        new MySqlServerVersion(new Version(10, 4, 32))
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Add this before Authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Introduction}/{id?}");

app.Run();
