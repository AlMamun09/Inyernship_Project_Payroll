using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Areas.Identity.Data;
using PayrollProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<PayrollUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // Turn off all password validation rules
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PayrollProject.Repositories.Interfaces.IEmployeeRepository, PayrollProject.Repositories.EmployeeRepository>();
builder.Services.AddScoped<PayrollProject.Repositories.Interfaces.IShiftRepository, PayrollProject.Repositories.ShiftRepository>();
builder.Services.AddScoped<PayrollProject.Repositories.Interfaces.IAttendanceRepository, PayrollProject.Repositories.AttendanceRepository>();
builder.Services.AddScoped<PayrollProject.Repositories.Interfaces.ILeaveRepository, PayrollProject.Repositories.LeaveRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
