   using Business.Interfaces;
using Business.Services;
using Repository.Interfaces;
using Repository.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDoctorBuss, DoctorBussiness>();
builder.Services.AddTransient<IDoctorRepo, DoctorRepo>();

builder.Services.AddTransient<IPatientBuss, PatientBusiness>();
builder.Services.AddTransient<IPatientRepo,PatientRepo>();

builder.Services.AddTransient<IAppointmentBuss, AppointmentBusiness>();
builder.Services.AddTransient<IAppointmentRepo,AppointementRepo>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
