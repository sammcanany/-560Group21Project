using Microsoft.AspNetCore.Identity;
using Group21ProjectMVC.Data;
using Group21ProjectMVC.Models;
using Group21ProjectMVC.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");;

builder.Services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
builder.Services.AddTransient<IFlightStore<ApplicationFlight>, FlightStore>();
builder.Services.AddTransient<ITicketStore<ApplicationTicket>, TicketStore>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
