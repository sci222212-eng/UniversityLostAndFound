using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityLostAndFound.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة اتصال قاعدة البيانات الأصلي الخاص بمشروعك
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. تفعيل خدمات الـ Identity للمستخدمين وربطها بكلاس قاعدة بياناتك
builder.Services.AddIdentity<UniversityLostAndFound.Data.ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
// 3. التعديل السحري الأول: إضافة دعم صفحات الـ Razor Pages لـ Identity
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// إعدادات بيئة العمل (تُترك كما هي)
if (app.Environment.IsDevelopment())
{
   // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. التعديل السحري الثاني: تفعيل نظام التحقق من الهوية والحماية بالترتيب الصحيح
app.UseAuthentication();
app.UseAuthorization();

// 5. توجيه الصفحات الافتراضية للموقع
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 6. التعديل السحري الثالث: ربط وتفعيل واجهات صفحات الـ Identity (تسجيل الدخول)
app.MapRazorPages();

app.Run();