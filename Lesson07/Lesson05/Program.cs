using Lesson05.Data;
using Lesson05.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// SQL Server
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// SQLite (Local storage)
//builder.Services.AddDbContext<UniversityDbContext>(options =>
//    options.UseSqlite("DataSource = Storage\\UniversityManagement.db"));
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
