using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using dotnetapp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnstring")));

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "displayCourses",
    pattern: "Enrollment/DisplayCoursesForEnrollment/{enrollmentId}",
    defaults: new { controller = "Enrollment", action = "DisplayCoursesForEnrollment" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Enrollment}/{action=DisplayAllCourses}/{id?}"); 

app.Run();
